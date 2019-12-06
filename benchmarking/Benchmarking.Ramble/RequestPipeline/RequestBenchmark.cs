using BenchmarkDotNet.Attributes;
using Benchmarking.Ramble.RequestPipeline.BaselineImplementation;
using Benchmarking.Ramble.RequestPipeline.Utility;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ramble.Common;
using Ramble.Services.Authorization;
using Ramble.Services.Pipeline;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Benchmarking.Ramble.RequestPipeline
{
    [MemoryDiagnoser]
    public class RequestBenchmark
    {
        private IServiceProvider _serviceProvider;

        [GlobalSetup]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            serviceCollection.AddMediatR(typeof(DummyRequestHandler));
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
            serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizationPipeline<,>));

            serviceCollection.AddScoped<IRequestContext, BenchmarkRequestContext>();

            var authorizationRuleEngines = typeof(BenchmarkDummyRule).Assembly.GetTypes()
                .Where(e => !e.IsAbstract && e.GetInterfaces().Contains(typeof(IAuthorizationRuleEngine)));

            foreach (var authorizationRuleEngine in authorizationRuleEngines)
                serviceCollection.AddScoped(authorizationRuleEngine);

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Benchmark]
        public async Task<bool> CreateAndSendRequest_Full()
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var dummyResult = await mediator.Send(new DummyRequestFull
            {
                AnswerToLife = 42,
                HasAnswerToTheQuestion = true
            });
                
            return dummyResult.IsSuccess;
        }

        [Benchmark]
        public async Task<bool> CreateAndSendRequest_Validation()
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var dummyResult = await mediator.Send(new DummyRequestValidation
            {
                AnswerToLife = 42,
                HasAnswerToTheQuestion = true
            });

            return dummyResult.IsSuccess;
        }

        [Benchmark]
        public async Task<bool> CreateAndSendRequest_Authorization()
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var dummyResult = await mediator.Send(new DummyRequestAuthorization
            {
                AnswerToLife = 42,
                HasAnswerToTheQuestion = true
            });

            return dummyResult.IsSuccess;
        }

        [Benchmark]
        public async Task<bool> CreateAndSendRequest_None()
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var dummyResult = await mediator.Send(new DummyRequestNone
            {
                AnswerToLife = 42,
                HasAnswerToTheQuestion = true
            });

            return dummyResult.IsSuccess;
        }
    }
}
