namespace LearningApp.Business.Interfaces.Helpers;

public interface IPasswordHasher
{
	/// <summary>
	/// Генерує хеш для переданого пароля.
	/// </summary>
	/// <param name="password">Пароль, який потрібно захешувати.</param>
	/// <returns>Повертає захешований пароль.</returns>
	string Generate(string password);

	/// <summary>
	/// Перевіряє, чи співпадає переданий пароль з захешованим значенням.
	/// </summary>
	/// <param name="password">Пароль, який потрібно перевірити.</param>
	/// <param name="hashedPassword">Захешований пароль для порівняння.</param>
	/// <returns>Повертає true, якщо пароль співпадає з хешем, інакше false.</returns>
	bool VerifyPassword(string password, string hashedPassword);
}
