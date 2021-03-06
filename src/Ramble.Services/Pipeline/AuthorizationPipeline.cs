﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ramble.Common.Requests.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ramble.Services.Pipeline
{
    public class AuthorizationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : RequestResult
    {
        private static readonly Dictionary<Type, RequestAuthorizer<TRequest>?> _cachedAuthorizers =
            new Dictionary<Type, RequestAuthorizer<TRequest>?>();

        private static readonly Dictionary<Type, Type> _cachedRuleEngineTypes =
            new Dictionary<Type, Type>();

        private readonly IRequestContext _requestContext;

        public AuthorizationPipeline(IRequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (TryGetRequestAuthorizer(out var requestAuthorizer) && requestAuthorizer.Rules.Any())
            {
                using var scope = _requestContext.ServiceProvider.CreateScope();
                foreach (var ruleContainer in requestAuthorizer.Rules)
                {
                    if (_cachedRuleEngineTypes.TryGetValue(ruleContainer.RuleType, out var engineType) && engineType != null)
                    {
                        var rule = ruleContainer.TransformAction(request);
                        var ruleEngine = (IAuthorizationRuleEngine) scope.ServiceProvider.GetRequiredService(engineType);
                        
                        if (!await ruleEngine.IsAuthorized(rule))
                            return (TResponse) new RequestResult(false, RequestResultErrorCode.Forbidden, "Unauthorized request");
                    }
                }
            }

            return await next();
        }

        private bool TryGetRequestAuthorizer([NotNullWhen(true)] out RequestAuthorizer<TRequest>? requestAuthorizer)
        {
            if (!_cachedAuthorizers.TryGetValue(typeof(TRequest), out requestAuthorizer))
            {
                var ruleAuthorizerType = typeof(TRequest).GetNestedTypes()
                    .Where(e => e.IsSubclassOf(typeof(RequestAuthorizer<TRequest>)))
                    .FirstOrDefault();

                lock (_cachedAuthorizers)
                {
                    requestAuthorizer = ruleAuthorizerType == null ? null
                        : Activator.CreateInstance(ruleAuthorizerType) as RequestAuthorizer<TRequest>;

                    _cachedAuthorizers.TryAdd(typeof(TRequest), requestAuthorizer);
                }

                if (requestAuthorizer != null)
                {
                    lock (_cachedRuleEngineTypes)
                    {
                        foreach (var ruleContainer in requestAuthorizer.Rules)
                        {
                            var ruleEngineType = ruleContainer.RuleType.GetNestedTypes()
                                .Where(e => !e.IsAbstract && e.GetInterfaces().Contains(typeof(IAuthorizationRuleEngine)))
                                .FirstOrDefault();

                            _cachedRuleEngineTypes.TryAdd(ruleContainer.RuleType, ruleEngineType);
                        }
                    }
                }
            }

            return requestAuthorizer != null;
        }
    }
}
