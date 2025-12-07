namespace GymManagement.MediatorLibrary;

public sealed record Error(string Code, string? Description = null)
{
    public static readonly Error None = new(string.Empty);
    public static implicit operator Result(Error error) => Result.Failure(error);
}

public abstract record ResultBase
{
    protected ResultBase(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Error Error { get; }
}

public sealed record Result(bool IsSuccess, Error Error)
    : ResultBase(IsSuccess, Error)
{
    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}

public sealed record Result<T>(bool IsSuccess, Error Error, T? Value = default)
    : ResultBase(IsSuccess, Error)
{
    public static Result<T> Success(T value) => new(true, Error.None, value);
    public static Result<T> Failure(Error error) => new(false, error);

    public static implicit operator Result<T>(Error error) => Failure(error);
}
