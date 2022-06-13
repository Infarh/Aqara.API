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


//const string response_json = @"{""code"":0,""message"":""Success"",""msgDetails"":null,""requestId"":""676.194.16551404702160737"",""result"":{""expiresIn"":""604800"",""openId"":""688199722857985578667851698177"",""accessToken"":""89a7ee6dd6698aebf7ed01f1bc548d00"",""refreshToken"":""baaa54b825fb07da282e8386eca9353d""}}";
//const string response_json = @"{
//    ""code"": 0,
//    ""message"": ""Success"",
//    ""msgDetails"": null,
//    ""requestId"": ""676.194.16551404702160737"",
//    ""result"": """" }";

//var response = JsonSerializer.Deserialize<AccessTokenResponse>(response_json);

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
    //var code = await client.RequestAuthorizationKey(config["Aqara:Account"], "24h");

    var token_info = await client.ObtainAccessToken(config["Aqara:VerificationCode"], config["Aqara:Account"]);
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