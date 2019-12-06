﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Ramble.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Pipeline
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class
    {
        private static readonly Dictionary<Type, RequestValidator<TRequest>> _cachedValidators =
            new Dictionary<Type, RequestValidator<TRequest>>();
        
        private readonly IRequestContext _requestContext;

        public ValidationPipeline(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (TryGetRequestValidator(out var requestValidator))
            {
                var validationResult = await requestValidator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return CreateErrorResponse(validationResult) as TResponse;
            }

            return await next();
        }

        private bool TryGetRequestValidator(out RequestValidator<TRequest> requestValidator)
        {
            if (!_cachedValidators.TryGetValue(typeof(TRequest), out requestValidator))
            {
                var validatorType = typeof(TRequest).GetNestedTypes()
                    .Where(e => e.IsSubclassOf(typeof(RequestValidator<TRequest>)))
                    .FirstOrDefault();

                lock (_cachedValidators)
                {
                    requestValidator = validatorType != null
                        ? Activator.CreateInstance(validatorType) as RequestValidator<TRequest>
                        : null;

                    _cachedValidators.TryAdd(typeof(TRequest), requestValidator);
                }
            }

            return requestValidator != null;
        }

        private RequestResult CreateErrorResponse(ValidationResult validationResult)
        {
            var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new RequestResult(false, RequestResultErrorCode.InvalidRequest, errorMessage);
        }
    }
}