using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aqara.API.TestConsole.Infrastructure.Extensions;

internal static class ServicesExtensions
{
    public static string GetConfigValue(this IServiceProvider services, string ConfigurationKey) =>
        services.GetRequiredService<IConfiguration>()[ConfigurationKey];

    public static T GetConfigValue<T>(this IServiceProvider services, string ConfigurationKey) =>
        services.GetRequiredService<IConfiguration>().GetValue<T>(ConfigurationKey);

    public static T GetConfigValue<T>(this IServiceProvider services, string ConfigurationKey, T DefaultValue) =>
        services.GetRequiredService<IConfiguration>().GetValue<T>(ConfigurationKey, DefaultValue);
}
