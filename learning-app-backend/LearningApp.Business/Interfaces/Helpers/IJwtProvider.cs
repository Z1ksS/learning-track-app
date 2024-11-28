using LearningApp.Business.DTOs;

namespace LearningApp.Business.Interfaces.Helpers;

public interface IJwtProvider
{
	string GenerateToken(UserDto user);
}
