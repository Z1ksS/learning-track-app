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
}
