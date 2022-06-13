namespace Aqara.API.TestConsole.Infrastructure;

internal static class FileExtensions
{
    public static IEnumerable<string> EnumLines(this FileInfo file)
    {
        using var reader = file.OpenText();
        while (!reader.EndOfStream)
            if (reader.ReadLine() is { Length: > 0 } line)
                yield return line;
    }
}