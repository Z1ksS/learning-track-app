using LearningApp.Business.Interfaces.Helpers;

namespace LearningApp.Business.Helpers;

/// <summary>
/// Клас для хешування паролів за допомогою алгоритму BCrypt.
/// Реалізує інтерфейс IPasswordHasher для генерації та перевірки паролів.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
	/// <summary>
	/// Генерує хеш для переданого пароля.
	/// </summary>
	/// <param name="password">Пароль, який потрібно захешувати.</param>
	/// <returns>Повертає захешований пароль.</returns>
	public string Generate(string password)
	{
		return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
	}

	/// <summary>
	/// Перевіряє, чи співпадає переданий пароль з захешованим значенням.
	/// </summary>
	/// <param name="password">Пароль, який потрібно перевірити.</param>
	/// <param name="hashedPassword">Захешований пароль для порівняння.</param>
	/// <returns>Повертає true, якщо пароль співпадає з хешем, інакше false.</returns>
	public bool VerifyPassword(string password, string hashedPassword) 
	{ 
		return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
	}
}
