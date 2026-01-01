using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

using Aqara.API.DTO;

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

#if WINDOWS
        Services.AddSingleton<IAccessTokenSource, AccessTokenWinRegistrySource>(); // регистрация только на Windows
#else
        Services.AddSingleton<IAccessTokenSource>(_ => new AccessTokenFileSource(token_file));
#endif

        var server_address = AqaraOptions.GetValue("Address", Servers.Russia);
        Services.AddHttpClient<IAqaraClient, AqaraClient>("Aqara", (s, client) => client.BaseAddress = new(server_address));

        Services.Configure<JsonSerializerOptions>(opt => opt.TypeInfoResolver = JsonTypeInfoResolver.Combine(DTOSerializerContext.Default, new DefaultJsonTypeInfoResolver()));

        Services.AddScoped<IDeviceManager, DeviceManager>();

        return Services;
    }
}
