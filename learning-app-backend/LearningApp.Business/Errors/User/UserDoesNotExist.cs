namespace LearningApp.Business.Errors.User;
public class UserDoesNotExist : UserError
{
	public UserDoesNotExist() : base($"Користувач з таким email не існує.") { }
}
