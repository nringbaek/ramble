using Benchmarking.Ramble.RequestPipeline.Utility;
using Microsoft.Extensions.Logging;
using Ramble.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmarking.Ramble.RequestPipeline.BaselineImplementation
{
    public class DummyRequestAuthorization : Request<DummyRequestAuthorization>
    {
        public int AnswerToLife { get; set; }
        public bool HasAnswerToTheQuestion { get; set; }

        public class Authorizer : RequestAuthorizer<DummyRequestAuthorization>
        {
            public Authorizer()
            {
                AddRule(e => new BenchmarkDummyRule
                {
                    HasAnswerToTheQuestion = e.HasAnswerToTheQuestion
                });
            }
        }
    }

    public class DummyRequestAuthorizationHandler : RequestHandler<DummyRequestAuthorization>
    {
        public DummyRequestAuthorizationHandler(ILogger<RequestHandler<DummyRequestAuthorization>> logger) : base(logger) { }
        public override Task<RequestResult> Handle(DummyRequestAuthorization request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Success());
        }
    }
}
