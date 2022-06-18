using System.Text.Json;

using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;
using Aqara.API.TestConsole;
using Aqara.API.TestConsole.Infrastructure;
using Aqara.API.TestConsole.Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = Host.CreateDefaultBuilder(args)
   .ConfigureAppConfiguration(cfg => cfg
       .AddUserSecrets(typeof(Program).Assembly)
       .AddCommandLine(args))
   .ConfigureServices((env, Services) =>
    {
        Services.AddOptions<AqaraClientConfig>()
           .Bind(env.Configuration.GetSection("Aqara"))
           .Validate(o => o.TokenStorageFile is { Length: > 0 }, "Не задан путь к файлу хранилища ключей")
           .Validate(o => o.AppId is { Length: > 0 })
           .Validate(o => o.KeyId is { Length: > 0 })
           .Validate(o => o.AppKey is { Length: > 0 })
           .ValidateDataAnnotations()
           .ValidateOnStart()
           .Services
           .AddTransient(s => s.GetRequiredService<IOptionsSnapshot<AqaraClientConfig>>().Value);

        Services.AddSingleton<IAccessTokenSource>(_ => new AccessTokenFileSource("AccessToken.json"));
        Services.AddHttpClient<AqaraClient>("Aqara", (s, client) => client.BaseAddress = new(s.GetConfigValue("Aqara:Address")));

        Services.Configure<JsonSerializerOptions>(opt => opt.AddContext<DTOSerializerContext>());
    })
   .Build();

var services = host.Services;
var config = host.Services.GetRequiredService<IConfiguration>();
await host.StartAsync();

var client = host.Services.GetRequiredService<AqaraClient>();

try
{
    //var code = await client.GetAuthorizationKey(config["Aqara:Account"], "24h");

    //var token_info = await client.GetAccessToken(config["Aqara:VerificationCode"], config["Aqara:Account"]);

    //var token = await client.RefreshAccessToken();

    //var positions = await client.GetPositions();
    //var devices = await client.GetDevicesByPosition("real1.930999863490531328");
    //var devices = await client.GetDevicesByPosition("real1.930999863490531328");

    const string termometr_id = "lumi.158d00071102f1";
    const string termometr_model_id = "lumi.weather.v1";
    //var resources = await client.GetDeviceModelFeatures(device_model_id);

    const string resource_temperature = "0.1.85";
    //var values = await client.GetDeviceFeatureStatistic(
    //    termometr_id, 
    //    new[] { resource_temperature }, 
    //    FeatureStatisticAggregationType.Average, 
    //    DateTime.Now.AddDays(-5));

    //var value = await client.GetDevicesFeaturesValues(new[] { ("lumi.158d00071102f1", new[] { "0.1.85" }) });
    //var value = await client.GetDeviceFeatureValue(termometr_id, resource_temperature);

    const string switch_id = "lumi.54ef4410001c67a8";
    //const string switch_model_id = "lumi.switch.l1aeu1";
    const string switch_feature_id = "4.1.85";

    await client.SetDevicesFeaturesValues(new[] { (switch_id, new[] { (switch_feature_id, 1d) }) });
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