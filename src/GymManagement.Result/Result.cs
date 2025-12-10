namespace GymManagement.Result;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);

    public static Error NotFound(string description) => new("NOT_FOUND", description);
    public static Error Validation(string description) => new("VALIDATION", description);
    public static Error Conflict(string description) => new("CONFLICT", description);
    public static Error Failure(string description) => new("FAILURE", description);
    public static Error Unexpected(string description) => new("UNEXPECTED", description);

    // public static implicit operator Result(Error error) => Result.Failure(error);
}

public record Result<T>
{
    public Result(T value)
    {
        Value = value;
        Error = null;
    }

    public Result(Error error)
    {
        Error = error;
        Value = default;
    }

    public T? Value { get; }
    public bool IsSuccess => Error == null;
    public bool IsFailure => !IsSuccess;

    public Error? Error { get; }

    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Failure(Error error) => new Result<T>(error);

    public TResult Map<TResult>(Func<T, TResult> OnSuccess, Func<Error, TResult> OnFailure)
    {
        return IsSuccess ? OnSuccess(Value!) : OnFailure(Error!);
    }
}
