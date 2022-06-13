using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;
using Aqara.API.TestConsole.Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder(args)
   .ConfigureAppConfiguration(cfg => cfg
       .AddUserSecrets(typeof(Program).Assembly)
       .AddCommandLine(args))
   .ConfigureServices((env, Services) =>
    {
        Services.Configure<AqaraClientConfig>(env.Configuration.GetSection("Aqara"));
        Services.AddHttpClient<AqaraClient>("Aqara", (s, client) => client.BaseAddress = new(s.GetConfigValue("Aqara:Address")));
    })
   .Build();

var services = host.Services;
var config = host.Services.GetRequiredService<IConfiguration>();
await host.StartAsync();

var client = host.Services.GetRequiredService<AqaraClient>();

try
{
    //var code = await client.RequestAuthorizationKey(config["Aqara:Account"]);



}
catch (AqaraAPIException error)
{
    Console.WriteLine(error);
    throw;
}

Console.ReadLine();

await host.StopAsync();
host.Dispose();

Console.WriteLine("Завершено");