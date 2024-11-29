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

	public async Task Delete(string email)
	{
		var user = await _learningAppDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

		if(user is not null)
		{
			_learningAppDbContext.Users.Remove(user);
			await _learningAppDbContext.SaveChangesAsync();
		}
	}

	public async Task Update(User user)
	{
		_learningAppDbContext.Users.Update(user);
		await _learningAppDbContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<User>> GetAllAsync()
	{
		var users = await _learningAppDbContext.Users.ToListAsync();

		return users;
	}

	public async Task<User?> GetByEmail(string email)
	{
		var user = await _learningAppDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
			
		return user;	
	}
}
