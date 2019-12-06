using FluentValidation;
using MediatR;
using Ramble.Services.Authorization;
using System;
using System.Collections.Generic;

namespace Ramble.Services
{
    public class RequestValidator<TRequest> : AbstractValidator<TRequest>
    {
    }

    public class RequestAuthorizerRule<TRequest, TRule>
    {
        public Type RuleType { get; set; }
        public Func<TRequest, TRule> TransformAction { get; set; }
    }

    public interface IRequestAuthorizer<TRequest, TRule>
    {
        List<RequestAuthorizerRule<TRequest, TRule>> Rules { get; }
        void AddRule(Type ruleType, Func<TRequest, TRule> transform);
    }

    public class RequestAuthorizer<TRequest> : IRequestAuthorizer<TRequest, IAuthorizationRule>
    {
        public List<RequestAuthorizerRule<TRequest, IAuthorizationRule>> Rules { get; }
            = new List<RequestAuthorizerRule<TRequest, IAuthorizationRule>>();

        public void AddRule<TRule>(Func<TRequest, TRule> transform) where TRule : IAuthorizationRule
            => AddRule(typeof(TRule), e => transform(e));

        public void AddRule(Type ruleType, Func<TRequest, IAuthorizationRule> transform)
        {
            Rules.Add(new RequestAuthorizerRule<TRequest, IAuthorizationRule>
            {
                TransformAction = transform,
                RuleType = ruleType
            });
        }
        
    }

    public class Request<TRequest> : IRequest<RequestResult> { }
    public class Request<TRequest, TResult> : IRequest<RequestResult<TResult>> { }
}
