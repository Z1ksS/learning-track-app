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
	private readonly IJwtProvider _jwtProvider;

	private const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

	public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider)
	{
		_passwordHasher = passwordHasher;
		_userRepository = userRepository;
		_jwtProvider = jwtProvider;
	}

	public async Task<Result<bool, ApplicationError>> RegisterUser(UserDto user)
	{
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
			Role = user.Role ?? "User"
		};

		await _userRepository.Add(newUser);
		return true;
	}

	public async Task<Result<string, ApplicationError>> Login(string email, string password)
	{
		var isValidEmail = Regex.IsMatch(email, pattern);

		if (!isValidEmail)
		{
			return new UserInvalidEmail();
		}

		var userWithEmailExists = await _userRepository.GetByEmail(email);

		if (userWithEmailExists is null)
		{
			return new UserDoesNotExist();
		}

		var passwordCheck = _passwordHasher.VerifyPassword(password, userWithEmailExists.PasswordHash);

		if (!passwordCheck)
		{
			return new UserPasswordIncorrect();
		}

		var userDto = new UserDto
		{
			Name = userWithEmailExists.Name,
			Email = userWithEmailExists.Email,
			Password = password,
			Role = userWithEmailExists.Role
		};

		var token = _jwtProvider.GenerateToken(userDto);

		return token;
	}

	public async Task<List<UserShortDto>> GetUsers()
	{
		var users = await _userRepository.GetAllAsync();

		var usersDto = users.Select(user => new UserShortDto
		{
			Name = user.Name,
			Email = user.Email,
			Role = user.Role
		});

		return usersDto.ToList();
	}

	public async Task<Result<UserDto, ApplicationError>> GetUserByEmail(string email)
	{
		var isValidEmail = Regex.IsMatch(email, pattern);

		if (!isValidEmail)
		{
			return new UserInvalidEmail();
		}

		var userWithEmailExists = await _userRepository.GetByEmail(email);

		if (userWithEmailExists is null)
		{
			return new UserDoesNotExist();
		}

		var userDto = new UserDto
		{
			Name = userWithEmailExists.Name,
			Email = userWithEmailExists.Email,
			Role = userWithEmailExists.Role
		};

		return userDto;
	}

	public async Task<Result<UserDto, ApplicationError>> UpdateUser(string email, UserDto user)
	{
		var isValidEmail = Regex.IsMatch(email, pattern);

		if (!isValidEmail)
		{
			return new UserInvalidEmail();
		}

		var userWithEmailExists = await _userRepository.GetByEmail(email);

		if (userWithEmailExists is null)
		{
			return new UserDoesNotExist();
		}

		var isValidInputedEmail = Regex.IsMatch(user.Email, pattern);

		if (!isValidInputedEmail)
		{
			return new UserInvalidEmail();
		}

		userWithEmailExists.Email = user.Email;
		userWithEmailExists.Name = user.Name;
		userWithEmailExists.Role = user.Role;

		await _userRepository.Update(userWithEmailExists);

		var userDto = new UserDto
		{
			Name = userWithEmailExists.Name,
			Email = userWithEmailExists.Email,
			Role = userWithEmailExists.Role
		};

		return userDto;
	}

	public async Task<Result<bool, ApplicationError>> DeleteUser(string email)
	{
		var isValidEmail = Regex.IsMatch(email, pattern);

		if (!isValidEmail)
		{
			return new UserInvalidEmail();
		}

		var userWithEmailExists = await _userRepository.GetByEmail(email);

		if (userWithEmailExists is null)
		{
			return new UserDoesNotExist();
		}

		await _userRepository.Delete(email);

		return true;
	}
}
