using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aqara.API.Infrastructure;

internal static class JsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions WithContext<TContext>(this JsonSerializerOptions options) 
        where TContext : JsonSerializerContext, new()
    {
        options.AddContext<TContext>();
        return options;
    }
}