namespace LearningApp.Business.Errors.User;
public class UserInvalidEmail : UserError
{
	public UserInvalidEmail() : base($"Введенний email не відповідає формату.") { }
}