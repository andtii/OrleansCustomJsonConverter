
using Orleans;
using Orleans.Runtime;
using Orleans.Hosting;
using Orleans.Configuration;
using OrleansMultiClusterExample.Web;
using OrleansMultiClusterExample.Web.Grains;
using OrleansMultiClusterExample.Web.Models;
using System.Globalization;
using Orleans.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);



builder.Host.UseOrleans((ctx, siloBuilder) =>
 {
     siloBuilder.UseLocalhostClustering()
       .Configure((Action<ClusterOptions>)(options =>
       {
             options.ClusterId = $"cluster1";
             options.ServiceId = "cluster1";
         }));

     siloBuilder.Services.AddSerializer(serializerBuilder =>
     {
         serializerBuilder.AddJsonSerializer(
             isSupported: type => type.Namespace.StartsWith("OrleansMultiClusterExample.Web")
             );
     });

 });


var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/test", async (IClusterClient cluster1Client) =>
{
    var testModel = new TestModel
    {
        Id = new TestId(new Guid("d325f848-1192-459b-bc9b-8042a8113117"))
    };

    //This works
    //var serialized = JsonSerializer.Serialize(testModel);
    //var backToModel = JsonSerializer.Deserialize<TestModel>(serialized);

    //This fails
    return await cluster1Client.GetGrain<ITestGrain>("").Dosomething(testModel);
});

await Task.WhenAll(app.RunAsync(), Task.Run(async () =>
{
    //Wait for the silo to start
    await Task.Delay(3000);
    var response = await new HttpClient().GetAsync("https://localhost:7069/test");
    var testModel = await response.Content.ReadFromJsonAsync<TestModel>();
    Console.WriteLine(testModel.Id);
    Console.ReadLine();

}));
