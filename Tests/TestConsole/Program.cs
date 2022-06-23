using System.Text.Json;

using Aqara.API;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;
using Aqara.API.TestConsole.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

//int[] values = { 1, 2, 3 };

//var rr = values switch
//{
//    null => "null",
//    [3, 2, 1] => "321",
//    //[var a, var b] => $"{a},{b}",
//    //[var a, var b, var c] => $"{a},{b},{c}",
//    _ => "default"
//};

var host = Host.CreateDefaultBuilder(args)
   .UseConsoleLifetime(opt => opt.SuppressStatusMessages = true)
   .ConfigureAppConfiguration(cfg => cfg
       .AddUserSecrets(typeof(Program).Assembly)
       .AddCommandLine(args))
   .ConfigureServices((env, Services) =>
    {
        //Services.AddOptions<AqaraClientConfig>()
        //   .Bind(env.Configuration.GetSection("Aqara"))
        //   .Validate(o => o.AppId is { Length: > 0 })
        //   .Validate(o => o.KeyId is { Length: > 0 })
        //   .Validate(o => o.AppKey is { Length: > 0 })
        //   .ValidateDataAnnotations()
        //   .ValidateOnStart()
        //   .Services
        //   .AddTransient(s => s.GetRequiredService<IOptionsSnapshot<AqaraClientConfig>>().Value);

        //Services.AddSingleton<IAccessTokenSource>(_ => new AccessTokenFileSource("AccessToken.json"));
        //Services.AddHttpClient<AqaraClient>("Aqara", (s, client) => client.BaseAddress = new(s.GetConfigValue("Aqara:Address")));

        //Services.Configure<JsonSerializerOptions>(opt => opt.AddContext<DTOSerializerContext>());

        Services.AddAqaraServices(env.Configuration.GetSection("Aqara"));
    })
   .Build();

var services = host.Services;
var config = services.GetRequiredService<IConfiguration>();
await host.StartAsync();

var token_store = services.GetRequiredService<IAccessTokenSource>();
var token = await token_store.GetAccessToken();
var client = services.GetRequiredService<IAqaraClient>();
var device_manager = services.GetRequiredService<IDeviceManager>();

try
{
    //var code = await client.GetAuthorizationKey(config["Aqara:Account"], "24h");

    //var token_info = await client.GetAccessToken("123456", config["Aqara:Account"]);

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
    const string switch_model_id = "lumi.switch.l1aeu1";
    const string switch_feature_id = "4.1.85";

    var features = await client.GetDeviceModelFeatures(switch_model_id);
    //await client.SetDevicesFeaturesValues(new[] { (switch_id, new[] { (switch_feature_id, 1d) }) });

    //await device_manager.Authorize("user@server.ru", "123456");

    //var devices = await device_manager.GetDevices();

    //var switch_device = await device_manager.GetDeviceById(switch_id);
    var switch_device = await device_manager.GetDeviceByName("Свет в коридоре");
    var switch_features = await switch_device.GetFeatures();
}
catch (AqaraAPIException error)
{
    Console.WriteLine(error);
    throw;
}
catch (Exception error)
{
    Console.WriteLine(error);
    throw;
}

Console.ReadLine();

await host.StopAsync();
host.Dispose();

Console.WriteLine("Завершено");