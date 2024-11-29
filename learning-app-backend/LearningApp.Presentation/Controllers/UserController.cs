using LearningApp.Business.DTOs;
using LearningApp.Business.Helpers;
using LearningApp.Business.Interfaces;
using LearningApp.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost("register")]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> RegisterUser(UserPresentation user)
	{
		var mappedUserDto = new UserDto
		{
			Name = user.Name,
			Email = user.Email,
			Password = user.Password
		};

		var registeredUser = await _userService.RegisterUser(mappedUserDto);

		return registeredUser.Match<IActionResult>(
			registered =>
			{
				return Ok();
			},
			error => BadRequest(error.Message)
		);
	}

	[HttpPost("login")]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> LoginUser(UserPresentationShort user)
	{
		var getToken = await _userService.Login(user.Email, user.Password);

		return getToken.Match<IActionResult>(
			token =>
			{
				HttpContext.Response.Cookies.Append("tasty-cookies", token);
				return Ok(token);
			},
			error => BadRequest(error.Message)
		);
	}

	[HttpPost("addUser")]
	[Authorize(Policy = "AdminPolicy")]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> AddUser(UserAddAdminPresentation user)
	{
		var mappedUserDto = new UserDto
		{
			Name = user.Name,
			Email = user.Email,
			Password = user.Password,
			Role = user.Role,
		};

		var registeredUser = await _userService.RegisterUser(mappedUserDto);

		return registeredUser.Match<IActionResult>(
			registered =>
			{
				return Ok();
			},
			error => BadRequest(error.Message)
		);
	}

	[HttpGet("getUsers")]
	[Authorize(Policy = "AdminPolicy")]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> GetUsers()
	{
		var users = await _userService.GetUsers();

		if (users == null)
		{
			return NotFound();
		}

		return Ok(users);
	}

	[HttpGet("getUserByEmail")]
	[Authorize]
	[ProducesResponseType(typeof(UserFullPresentation), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	public async Task<IActionResult> GetUserByEmail(string email)
	{
		var emailFromToken = User.Claims.FirstOrDefault(c =>
			c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

		if (emailFromToken == null || !emailFromToken.Equals(email, StringComparison.OrdinalIgnoreCase))
		{
			return StatusCode(StatusCodes.Status403Forbidden, "Ви можете отримати доступ тільки до власних даних користувача");
		}

		var user = await _userService.GetUserByEmail(email);

		return user.Match<IActionResult>(
			found =>
			{
				var userPresentation = new UserFullPresentation
				{
					Email = found.Email,
					Name = found.Name,
					Role = found.Role
				};

				return Ok(userPresentation);
			},
			error => NotFound(error.Message)
		);
	}

	[HttpDelete("deleteUser")]
	[Authorize(Policy = "AdminPolicy")]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteUser(string email)
	{
		var user = await _userService.DeleteUser(email);

		return user.Match<IActionResult>(
			deleted =>
			{
				return Ok(deleted);
			},
			error => NotFound(error.Message)
		);
	}

	[HttpPatch("updateUser")]
	[Authorize(Policy = "AdminPolicy")]
	[ProducesResponseType(typeof(UserFullPresentation), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateUser(string email, UserFullPresentation user)
	{
		var userDto = new UserDto
		{
			Email = user.Email,
			Name = user.Name,
			Role = user.Role
		};

		var userUpdate = await _userService.UpdateUser(email, userDto);

		return userUpdate.Match<IActionResult>(
			updated =>
			{
				var userPresentation = new UserFullPresentation
				{
					Email = user.Email,
					Name = user.Name,
					Role = user.Role
				};

				return Ok(userPresentation);
			},
			error => NotFound(error.Message)
		);
	}
}
