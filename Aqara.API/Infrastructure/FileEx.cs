namespace Aqara.API.Infrastructure;

internal static class FileEx
{
    extension(File)
    {
        public static FileStream Create(string FileName, int bufferSize, bool useAsync) =>
            new(
                FileName,
                FileMode.Create,
                FileAccess.ReadWrite,
                FileShare.None,
                bufferSize,
                useAsync
            );
    }
}
