using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using Template.Application;
using UnitTesting.Base;
using Xunit.Abstractions;

namespace Template.UnitTests
{

    public class UnitTest1(ITestOutputHelper output) : UnitTestBase
    {   
        private readonly ITestOutputHelper _output = output;

        [Fact]
        public async Task Test1()
        {
            var logger = new AccumulationLogger();

            await templateClient.Service.GetAllCars();

            var config = ManualConfig.Create(DefaultConfig.Instance)
                .AddLogger(logger)
                .WithOptions(ConfigOptions.DisableOptimizationsValidator);

            var benchmark = new HeavyBenchmarks();
            benchmark.SetParameters(templateClient); // Pass parameters

            var summary = BenchmarkRunner.Run<HeavyBenchmarks>(config);

            // write benchmark summary
            _output.WriteLine(logger.GetLog());

        }
    }
}