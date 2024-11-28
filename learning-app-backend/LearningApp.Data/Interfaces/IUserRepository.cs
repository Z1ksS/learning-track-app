using LearningApp.Data.Entities.UserData;

namespace LearningApp.Data.Interfaces;

public interface IUserRepository
{
	Task Add(User user);
	Task<User?> GetByEmail(string email);
	Task<IEnumerable<User>> GetAllAsync();
}
