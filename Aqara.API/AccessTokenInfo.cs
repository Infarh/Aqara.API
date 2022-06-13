using System.Text.Json;

namespace Aqara.API;

public record AccessTokenInfo(string AccessToken, string RefreshToken)
{
    public static async Task<AccessTokenInfo> ReadFromFileAsync(string FileName, CancellationToken Cancel = default)
    {
        if (!File.Exists(FileName)) throw new FileNotFoundException("Файл данных токена не найден", FileName);

        await using var reader = File.OpenRead(FileName);
        return await JsonSerializer
               .DeserializeAsync<AccessTokenInfo>(reader, cancellationToken: Cancel)
               .ConfigureAwait(false)
            ?? throw new InvalidOperationException("Не удалось прочитать данные токена");
    }
    
    public static AccessTokenInfo ReadFromFile(string FileName)
    {
        if (!File.Exists(FileName)) throw new FileNotFoundException("Файл данных токена не найден", FileName);

        using var reader = File.OpenRead(FileName);
        return JsonSerializer
               .Deserialize<AccessTokenInfo>(reader)
            ?? throw new InvalidOperationException("Не удалось прочитать данные токена");
    }

    public async Task SaveToFileAsync(string FileName, CancellationToken Cancel = default)
    {
        await using var writer = File.OpenWrite(FileName);
        await JsonSerializer
           .SerializeAsync(writer, this, new JsonSerializerOptions { WriteIndented = true }, Cancel)
           .ConfigureAwait(false);
    }

    public void SaveToFile(string FileName)
    {
        using var writer = File.OpenWrite(FileName);
        JsonSerializer.Serialize(writer, this, new JsonSerializerOptions { WriteIndented = true });
    }
}
