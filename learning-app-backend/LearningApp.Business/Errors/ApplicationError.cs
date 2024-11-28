namespace LearningApp.Business.Errors;

public abstract class ApplicationError
{
	public string? Message { get; set; }
	public ApplicationError(string? message = null)
	{
		Message = message;
	}
}
