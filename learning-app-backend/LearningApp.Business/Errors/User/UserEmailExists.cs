namespace LearningApp.Business.Errors.User;

public class UserEmailExists : UserError
{
	public UserEmailExists() : base($"Користувач з таким email вже існує.") { }
}
