

using Orleans;
using OrleansMultiClusterExample.Web.Models;

namespace OrleansMultiClusterExample.Web.Grains
{
    public class TestGrain : ITestGrain
    {
        private readonly IClusterClient clusterClient;

        public TestGrain(IClusterClient clusterClient)
        {
            this.clusterClient = clusterClient;
        }

        public Task<TestModel> Dosomething(TestModel weather)
        {
            return Task.FromResult(weather);
        }

    }
}