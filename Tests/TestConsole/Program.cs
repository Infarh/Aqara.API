using Aqara.API.Exceptions.Base;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

/*
Перед началом работы нужно добавить в секреты пользователя (User Secrets) следующие параметры:
{
  "Aqara": {
    "AppId": "app-id...",
    "AppKey": "app-key...",
    "KeyId": "key-id...",
    "Account": "user_email_account@server.ru"
  }
}

Данные нужно взять с сайта https://developer.aqara.com
Для этого нужно зарегистрироваться/залогиниться в https://developer.aqara.com/login?router=%2Fconsole%2Foverview%2Faccount
После этого в разделе "Manage Project" создать проект. Проект даст значение appId.
Зайдя в Details проекта в разделе Key management
- Key Id = KeyId
- Key = AppKey
*/

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
    if (await client.IsAuthorisationNeeded())
    {
        Console.WriteLine("Требуется авторизация");
        var account = config["Aqara:Account"] ?? throw new InvalidOperationException("Не задан аккаунт");

        if (await client.GetAuthorizationKey(account, "24h") is not { Length: > 0 } code)
        {
            Console.WriteLine($"Код авторизации был отправлен на электронную почту {account}.");
            Console.Write("Введите полученный код:");
            if (Console.ReadLine() is not { Length: > 0 } input_code)
            {
                Console.WriteLine("Код авторизации отсутствует. Дальнейшая работа невозможна.");
                return;
            }

            code = input_code;
        }
        else
            Console.WriteLine($"Код авторизации: {code}");

        if (await client.GetAccessToken(code, config["Aqara:Account"]) is not { } token_info)
        {
            Console.WriteLine("Ошибка авторизации. Не получен токен доступа.");
            return;
        }
    }

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