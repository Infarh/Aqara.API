using Microsoft.Extensions.Logging;

namespace Aqara.API;

/// <summary>Логгирование</summary>
internal static partial class Log
{
    /// <summary>Не удалось открыть сокет</summary>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Critical,
        Message = "Could not open socket to `{hostName}`")]
    public static partial void CouldNotOpenSocket(
        this ILogger logger, string hostName);
}
