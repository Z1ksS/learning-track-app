using LearningApp.Business.DTOs;
using LearningApp.Business.Interfaces.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LearningApp.Business.Helpers;

public class JwtProvider : IJwtProvider
{
	private readonly JwtOptions _jwtOptions;
	public JwtProvider(IOptions<JwtOptions> jwtOptions)
	{
		_jwtOptions = jwtOptions.Value;
	}

	public string GenerateToken(UserDto user)
	{
		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), 
			SecurityAlgorithms.HmacSha256
		);

		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.Email),
			new Claim(ClaimTypes.Role, user.Role) // Додавання ролі
		};

		var token = new JwtSecurityToken(
			claims: claims,
			signingCredentials: signingCredentials,
			expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours)
		);

		var tokenToString = new JwtSecurityTokenHandler().WriteToken(token);

		return tokenToString;
	}
}
