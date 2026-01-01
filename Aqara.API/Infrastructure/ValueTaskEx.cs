namespace Aqara.API.Infrastructure;

internal static class ValueTaskEx
{
    extension(ValueTask)
    {
        public static ValueTask<T?> Null<T>() => ValueTask.FromResult(default(T));
    }
}
