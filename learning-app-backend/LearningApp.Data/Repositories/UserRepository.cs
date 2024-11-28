using LearningApp.Data.Entities.UserData;
using LearningApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.Data.Repositories;
public class UserRepository : IUserRepository
{
	private readonly LearningAppDbContext _learningAppDbContext;

	public UserRepository(LearningAppDbContext learningAppDbContext)
	{
		_learningAppDbContext = learningAppDbContext;
	}

	public async Task Add(User user)
	{
		await _learningAppDbContext.AddAsync(user);
		await _learningAppDbContext.SaveChangesAsync();
	}

	public async Task<User?> GetByEmail(string email)
	{
		var user = await _learningAppDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
			
		return user;	
	}
}
