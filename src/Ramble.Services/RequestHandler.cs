using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services
{
    public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest, RequestResult> where TRequest : Request<TRequest>
    {
        protected ILogger<RequestHandler<TRequest>> Logger { get; }

        public RequestHandler(ILogger<RequestHandler<TRequest>> logger)
        {
            Logger = logger;
        }

        public abstract Task<RequestResult> Handle(TRequest request, CancellationToken cancellationToken);

        public RequestResult Success() => new RequestResult(true, RequestResultErrorCode.None, null!);

        public RequestResult Error() => new RequestResult(false, RequestResultErrorCode.None, null!);
        public RequestResult Error(RequestResultErrorCode errorCode) => new RequestResult(false, errorCode, null!);
        public RequestResult Error(RequestResultErrorCode errorCode, string errorMessage) => new RequestResult(false, errorCode, errorMessage);

        public RequestResult Error(RequestResult result) => new RequestResult(false, result.ErrorCode, result.ErrorMessage);

    }

    public abstract class RequestHandler<TRequest, TValue> : IRequestHandler<TRequest, RequestResult<TValue>> where TRequest : IRequest<RequestResult<TValue>>
    {
        protected ILogger<RequestHandler<TRequest, TValue>> Logger { get; }

        public RequestHandler(ILogger<RequestHandler<TRequest, TValue>> logger)
        {
            Logger = logger;
        }

        public abstract Task<RequestResult<TValue>> Handle(TRequest request, CancellationToken cancellationToken);

        public RequestResult<TValue> Success() => new RequestResult<TValue>(true, RequestResultErrorCode.None, null!);
        public RequestResult<TValue> Success(TValue result) => new RequestResult<TValue>(result, true, RequestResultErrorCode.None, null!);

        public RequestResult<TValue> Error() => new RequestResult<TValue>(false, RequestResultErrorCode.None, null!);
        public RequestResult<TValue> Error(TValue result) => new RequestResult<TValue>(result, false, RequestResultErrorCode.None, null!);
        public RequestResult<TValue> Error(RequestResultErrorCode errorCode) => new RequestResult<TValue>(false, errorCode, null!);
        public RequestResult<TValue> Error(TValue result, RequestResultErrorCode errorCode) => new RequestResult<TValue>(result, false, errorCode, null!);
        public RequestResult<TValue> Error(RequestResultErrorCode errorCode, string errorMessage) => new RequestResult<TValue>(false, errorCode, errorMessage);
        public RequestResult<TValue> Error(TValue result, RequestResultErrorCode errorCode, string errorMessage) => new RequestResult<TValue>(result, false, errorCode, errorMessage);

        public RequestResult<TValue> Error(RequestResult<TValue> result) => new RequestResult<TValue>(false, result.ErrorCode, result.ErrorMessage);
    }

    public abstract class RequestHandler<TRequest, TSuccessValue, TErrorValue> : IRequestHandler<TRequest, RequestResult<TSuccessValue, TErrorValue>> where TRequest : IRequest<RequestResult<TSuccessValue, TErrorValue>>
    {
        protected ILogger<RequestHandler<TRequest, TSuccessValue, TErrorValue>> Logger { get; }

        public RequestHandler(ILogger<RequestHandler<TRequest, TSuccessValue, TErrorValue>> logger)
        {
            Logger = logger;
        }

        public abstract Task<RequestResult<TSuccessValue, TErrorValue>> Handle(TRequest request, CancellationToken cancellationToken);

        public RequestResult<TSuccessValue, TErrorValue> Success(TSuccessValue successValue) => new RequestResult<TSuccessValue, TErrorValue>(successValue, RequestResultErrorCode.None, null!);

        public RequestResult<TSuccessValue, TErrorValue> Error() => new RequestResult<TSuccessValue, TErrorValue>(false, RequestResultErrorCode.None, null!);
        public RequestResult<TSuccessValue, TErrorValue> Error(TErrorValue errorValue) => new RequestResult<TSuccessValue, TErrorValue>(errorValue, RequestResultErrorCode.None, null!);
        public RequestResult<TSuccessValue, TErrorValue> Error(RequestResultErrorCode errorCode) => new RequestResult<TSuccessValue, TErrorValue>(false, errorCode, null!);
        public RequestResult<TSuccessValue, TErrorValue> Error(TErrorValue errorValue, RequestResultErrorCode errorCode) => new RequestResult<TSuccessValue, TErrorValue>(errorValue, errorCode, null!);
        public RequestResult<TSuccessValue, TErrorValue> Error(RequestResultErrorCode errorCode, string errorMessage) => new RequestResult<TSuccessValue, TErrorValue>(false, errorCode, errorMessage);
        public RequestResult<TSuccessValue, TErrorValue> Error(TErrorValue errorValue, RequestResultErrorCode errorCode, string errorMessage) => new RequestResult<TSuccessValue, TErrorValue>(errorValue, errorCode, errorMessage);

        public RequestResult<TSuccessValue, TErrorValue> Error(RequestResult<TSuccessValue, TErrorValue> result) => new RequestResult<TSuccessValue, TErrorValue>(false, result.ErrorCode, result.ErrorMessage);
    }
}
