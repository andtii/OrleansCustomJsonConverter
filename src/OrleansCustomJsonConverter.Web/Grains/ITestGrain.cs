using Orleans;
using OrleansMultiClusterExample.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansMultiClusterExample.Web.Grains
{
    public interface ITestGrain : IGrainWithStringKey
    {
        public Task<TestModel> Dosomething(TestModel weather);

    }
}
