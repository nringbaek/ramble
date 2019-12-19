using BenchmarkDotNet.Running;
using Benchmarking.Ramble.RequestPipeline;
using System;
using System.Threading.Tasks;

namespace Benchmarking.Ramble
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<RequestBenchmark>();
        }
    }
}
