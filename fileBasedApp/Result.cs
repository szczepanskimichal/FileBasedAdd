namespace fileBasedApp
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string Error { get; }

        private Result(bool isSuccess, T value, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, "");
        }

        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default!, error);
        }

        public Result<TOut> Map<TOut>(Func<T, TOut> mapper)
        {
            if (!IsSuccess)
            {
                return Result<TOut>.Failure(Error);
            }
            return Result<TOut>.Success(
                mapper(Value));
        }

        public Result<TOut> FlatMap<TOut>(
            Func<T, Result<TOut>> mapper)
        {
            if (!IsSuccess)
            {
                return Result<TOut>.Failure(Error);
            }

            return mapper(Value);
        }
    }
}