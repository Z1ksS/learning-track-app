using LearningApp.Business.DTOs;
using LearningApp.Business.Errors;
using LearningApp.Business.Errors.User;
using LearningApp.Business.Interfaces;
using LearningApp.Business.Interfaces.Helpers;
using LearningApp.Business.Results;
using LearningApp.Data.Entities.UserData;
using LearningApp.Data.Interfaces;
using System.Text.RegularExpressions;

namespace LearningApp.Business.Services;

public class UserService : IUserService
{
	private readonly IPasswordHasher _passwordHasher;
	private readonly IUserRepository _userRepository;

	public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository)
	{
		_passwordHasher = passwordHasher;
		_userRepository = userRepository;
	}

	public async Task<Result<bool, ApplicationError>> RegisterUser(UserDto user)
	{
		string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

		var isValidEmail = Regex.IsMatch(user.Email, pattern);

		if (!isValidEmail) {
			return new UserInvalidEmail();
		}

		var userWithEmailExists = await _userRepository.GetByEmail(user.Email);

		if(userWithEmailExists is not null)
		{
			return new UserEmailExists();
		}

		var hashedPassword = _passwordHasher.Generate(user.Password);

		var newUser = new User
		{
			Name = user.Name,
			Email = user.Email,	
			PasswordHash = hashedPassword,
			Role = "User"
		};

		await _userRepository.Add(newUser);
		return true;
	}
}
