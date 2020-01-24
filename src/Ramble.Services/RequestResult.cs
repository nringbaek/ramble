namespace Ramble.Services
{
    /// <summary>
    /// The base request result class. Used to indicate whether a request was successful or not
    /// </summary>
    public class RequestResult
    {
        public bool IsSuccess { get; private set; }

        public bool IsError => !IsSuccess;
        public string ErrorMessage { get; private set; }
        public RequestResultErrorCode ErrorCode { get; private set; }

        public RequestResult(bool isSuccess, RequestResultErrorCode errorCode, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }

    /// <summary>
    /// RequestResult class used when the request either return some data at success or error.
    /// If both the success and error result object is the same, this class can be used too.
    /// 
    /// See <see cref="RequestResult{TSuccessValue, TErrorValue}"/> if success and error results are different.
    /// </summary>
    /// <typeparam name="TValue">The result data type</typeparam>
    public class RequestResult<TValue> : RequestResult
    {
        public TValue Value { get; private set; } = default!;

        public RequestResult(bool isSuccess, RequestResultErrorCode errorCode, string errorMessage)
            : base(isSuccess, errorCode, errorMessage) { }

        public RequestResult(TValue value, bool isSuccess, RequestResultErrorCode errorCode, string errorMessage)
            : base(isSuccess, errorCode, errorMessage) => Value = value;
    }

    /// <summary>
    /// RequestResult class used when different results is returned for successful and error requests
    /// </summary>
    /// <typeparam name="TSuccessValue">The success type</typeparam>
    /// <typeparam name="TErrorValue">The error type</typeparam>
    public class RequestResult<TSuccessValue, TErrorValue> : RequestResult
    {
        public TSuccessValue SuccessValue { get; private set; } = default!;
        public TErrorValue ErrorValue { get; private set; } = default!;

        public RequestResult(bool isSuccess, RequestResultErrorCode errorCode, string errorMessage)
            : base(isSuccess, errorCode, errorMessage) { }

        public RequestResult(TSuccessValue successValue, RequestResultErrorCode errorCode, string errorMessage)
            : base(true, errorCode, errorMessage) => SuccessValue = successValue;

        public RequestResult(TErrorValue errorValue, RequestResultErrorCode errorCode, string errorMessage)
            : base(false, errorCode, errorMessage) => ErrorValue = errorValue;
    }
}
