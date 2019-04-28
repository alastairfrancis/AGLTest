namespace AGLTest.Common.Models
{
    /// <summary>
    /// Generic result type from service calls.
    /// If Success is false, the reason is defined in the Error field.
    /// </summary>
    public class Result<T>
    {
        public bool Success { get; private set; }
        public string Error { get; private set; }
        public T Data { get; private set; }

        /// <summary>
        /// Create object on success
        /// </summary>
        public static Result<T> OnSuccess(T data)
        {
            return new Result<T>()
            {
                Success = true,
                Error = null,
                Data = data
            };
        }

        /// <summary>
        /// Create object on failure
        /// </summary>
        public static Result<T> OnFail(string errorReason)
        {
            return new Result<T>()
            {
                Success = false,
                Error = errorReason,
                Data = default(T)
            };
        }
    }
}
