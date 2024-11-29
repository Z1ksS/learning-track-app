using LearningApp.Business.DTOs;
using LearningApp.Business.Errors;
using LearningApp.Business.Results;

namespace LearningApp.Business.Interfaces;

public interface IUserService
{
	Task<Result<bool, ApplicationError>> RegisterUser(UserDto user);
	Task<Result<string, ApplicationError>> Login(string email,  string password);
	Task<List<UserShortDto>> GetUsers();
	Task<Result<UserDto, ApplicationError>> GetUserByEmail(string email);
	Task<Result<UserDto, ApplicationError>> UpdateUser(string email, UserDto user);
	Task<Result<bool, ApplicationError>> DeleteUser(string email);
 }
