namespace WebApiTutorial250818.WebApi.Common
{
    public enum ErrorType { NotFound, Validation, Conflict, Forbidden, Unauthorized, Unexpected }

    public sealed record Error(ErrorType Type, string Code, string Message);

    public class Result
    {
        public bool IsSuccess { get; }
        public Error? Error { get; }

        protected Result(bool ok, Error? error) { IsSuccess = ok; Error = error; }

        public static Result Success() => new(true, null);
        public static Result Fail(Error error) => new(false, error);
    }

    public sealed class Result<T> : Result
    {
        public T? Value { get; }
        private Result(bool ok, T? value, Error? error) : base(ok, error) { Value = value; }

        public static Result<T> Success(T value) => new(true, value, null);
        public static new Result<T> Fail(Error error) => new(false, default, error);
    }
}