using LearningApp.Data.Entities.UserData;

namespace LearningApp.Data.Interfaces;

public interface IUserRepository
{
	Task Add(User user);
	Task Update(User user);
	Task Delete(string email);
	Task<User?> GetByEmail(string email);
	Task<IEnumerable<User>> GetAllAsync();
}
