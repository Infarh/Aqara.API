using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Aqara.API.Infrastructure;

internal static class JsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions WithContext<TContext>(this JsonSerializerOptions options)
        where TContext : JsonSerializerContext, new()
    {
        ArgumentNullException.ThrowIfNull(options);

        var context_resolver = new JsonSerializerContextResolver(new TContext());
        if (options.TypeInfoResolver is not { } current_resolver)
        {
            options.TypeInfoResolver = context_resolver;
            return options;
        }

        options.TypeInfoResolver = new CompositeJsonTypeInfoResolver(current_resolver, context_resolver);
        return options;
    }

    private sealed record JsonSerializerContextResolver(JsonSerializerContext Context)
        : IJsonTypeInfoResolver
    {
        public JsonTypeInfo? GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            try
            {
                return Context.GetTypeInfo(type);
            }
            catch
            {
                return null;
            }
        }
    }

    private sealed record CompositeJsonTypeInfoResolver(IJsonTypeInfoResolver First, IJsonTypeInfoResolver Second)
        : IJsonTypeInfoResolver
    {
        public JsonTypeInfo? GetTypeInfo(Type type, JsonSerializerOptions options) =>
            First.GetTypeInfo(type, options) is { } info
            ? info
            : Second.GetTypeInfo(type, options);
    }
}