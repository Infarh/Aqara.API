namespace Aqara.API.Infrastructure;

internal static class FileNotFoundExceptionEx
{
    extension(FileNotFoundException)
    {
        public static void ThrowIfNotExists(string FileName, string? Message = null)
        {
            if (!File.Exists(FileName))
                throw new FileNotFoundException(Message ?? "Файл не найден", FileName);
        }
    }
}
