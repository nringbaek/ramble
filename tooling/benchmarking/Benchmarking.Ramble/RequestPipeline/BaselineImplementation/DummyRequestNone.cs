using Microsoft.Extensions.Logging;
using Ramble;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmarking.Ramble.RequestPipeline.BaselineImplementation
{
    public class DummyRequestNone : Request<DummyRequestNone>
    {
        public int AnswerToLife { get; set; }
        public bool HasAnswerToTheQuestion { get; set; }
    }

    public class DummyRequestNoneHandler : RequestHandler<DummyRequestNone>
    {
        public DummyRequestNoneHandler(ILogger<RequestHandler<DummyRequestNone>> logger) : base(logger) { }
        public override Task<RequestResult> Handle(DummyRequestNone request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Success());
        }
    }
}
