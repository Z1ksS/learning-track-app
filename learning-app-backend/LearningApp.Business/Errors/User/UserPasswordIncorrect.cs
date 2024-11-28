namespace LearningApp.Business.Errors.User;
public class UserPasswordIncorrect : UserError
{
	public UserPasswordIncorrect() : base($"Невірний пароль") { }
}
