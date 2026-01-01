namespace Aqara.API.Infrastructure;

internal static class OperatingSystemEx
{
    extension(OperatingSystem)
    {
        /// <summary>Проверить, что текущая платформа является Windows</summary>
        /// <exception cref="PlatformNotSupportedException">Если платформа не поддерживает Windows API</exception>
        public static void CheckWindows(string? Message = null)
        {
            if (!OperatingSystem.IsWindows())
                throw new PlatformNotSupportedException(Message ?? "This operation is only supported on Windows.");
        }
    }
}
