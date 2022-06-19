using Microsoft.Extensions.Logging;

namespace Aqara.API;

internal static partial class Log
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Critical,
        Message = "Could not open socket to `{hostName}`")]
    public static partial void CouldNotOpenSocket(
        this ILogger logger, string hostName);
}
