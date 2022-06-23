using Aqara.API.DTO;
using System.Text.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aqara.API;

public static class ServicesEx
{
    public static IServiceCollection AddAqaraServices(this IServiceCollection Services, IConfiguration AqaraOptions)
    {
        Services.AddOptions<AqaraClientConfig>()
           .Bind(AqaraOptions)
           .Validate(o => o.AppId is { Length: > 0 })
           .Validate(o => o.KeyId is { Length: > 0 })
           .Validate(o => o.AppKey is { Length: > 0 })
           .ValidateDataAnnotations()
            //.ValidateOnStart()
           .Services
           .AddTransient(s => s.GetRequiredService<IOptionsSnapshot<AqaraClientConfig>>().Value);

        var token_file = AqaraOptions.GetValue("TokenStorageFile", "AccessToken.json");
        //Services.AddSingleton<IAccessTokenSource>(_ => new AccessTokenFileSource(token_file));
        Services.AddSingleton<IAccessTokenSource, AccessTokenWinRegistrySource>();

        var server_address = AqaraOptions.GetValue("Address", Servers.Russia);
        Services.AddHttpClient<IAqaraClient, AqaraClient>("Aqara", (s, client) => client.BaseAddress = new(server_address));

        Services.Configure<JsonSerializerOptions>(opt => opt.AddContext<DTOSerializerContext>());

        Services.AddScoped<IDeviceManager, DeviceManager>();

        return Services;
    }
}
