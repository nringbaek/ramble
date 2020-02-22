using Microsoft.Extensions.Logging;
using Ramble;
using System.Threading;
using System.Threading.Tasks;

namespace Benchmarking.Ramble.RequestPipeline.BaselineImplementation
{
    public class DummyRequestValidation : Request<DummyRequestValidation>
    {
        public int AnswerToLife { get; set; }
        public bool HasAnswerToTheQuestion { get; set; }

        public class Validator : RequestValidator<DummyRequestValidation>
        {
            public Validator()
            {
                RuleFor(e => e.AnswerToLife).Equals(42);
            }
        }
    }

    public class DummyRequestValidationHandler : RequestHandler<DummyRequestValidation>
    {
        public DummyRequestValidationHandler(ILogger<RequestHandler<DummyRequestValidation>> logger) : base(logger) { }
        public override Task<RequestResult> Handle(DummyRequestValidation request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Success());
        }
    }
}
