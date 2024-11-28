using LearningApp.Business.Errors;

namespace LearningApp.Business.Results;

public readonly struct Result<TValue, TError> where TError : ApplicationError
{
	private readonly TValue? value;
	private readonly TError? error;

	public bool isError { get; }
	public bool isSuccess => !isError;

	private Result(TValue value)
	{
		this.isError = false;

		this.value = value;
		this.error = default;
	}

	private Result(TError error)
	{
		isError = true;

		this.value = default;
		this.error = error;
	}

	public static implicit operator Result<TValue, TError>(TValue value) => new(value);

	public static implicit operator Result<TValue, TError>(TError error) => new(error);

	public TResult Match<TResult>(
		Func<TValue, TResult> success,
		Func<TError, TResult> failure) =>
		!isError ? success(value!) : failure(error!);
}