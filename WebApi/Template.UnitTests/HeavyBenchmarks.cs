using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Application;
using UnitTesting.Base;

namespace Template.UnitTests
{
    [MemoryDiagnoser]
    public class HeavyBenchmarks
    {

        private ITemplateClient _templateClient;
        private string _imageName;

        public void SetParameters(ITemplateClient client)
        {
            _templateClient = client;
        }
        [Benchmark]
        public async Task ProcessRequest()
        {
            await _templateClient.Command.DeleteImagesAsync("test", DateTime.Now);
        }
    }
}
