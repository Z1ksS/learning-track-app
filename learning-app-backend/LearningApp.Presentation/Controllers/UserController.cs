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
}
