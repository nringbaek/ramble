using Benchmarking.Ramble.RequestPipeline.Utility;
using Microsoft.Extensions.Logging;
using Ramble;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmarking.Ramble.RequestPipeline.BaselineImplementation
{
    public class DummyRequestFull : Request<DummyRequestFull>
    {
        public int AnswerToLife { get; set; }
        public bool HasAnswerToTheQuestion { get; set; }

        public class Validator : RequestValidator<DummyRequestFull>
        {
            public Validator()
            {
                RuleFor(e => e.AnswerToLife).Equals(42);
            }
        }

        public class Authorizer : RequestAuthorizer<DummyRequestFull>
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

    public class DummyRequestHandler : RequestHandler<DummyRequestFull>
    {
        public DummyRequestHandler(ILogger<RequestHandler<DummyRequestFull>> logger) : base(logger) { }
        public override Task<RequestResult> Handle(DummyRequestFull request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Success());
        }
    }
}
