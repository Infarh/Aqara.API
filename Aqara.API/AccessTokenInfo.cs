using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aqara.API;

public record AccessTokenInfo(string AccessToken, string RefreshToken, int Expires, string OpenId, DateTime CreationTime)
{
    [JsonIgnore]
    public DateTime ExpiresTime => CreationTime.AddSeconds(Expires);

    [JsonIgnore]
    public DateTime RefreshTokenExpire => CreationTime.AddDays(30);

    [JsonIgnore]
    public bool IsExpire => DateTime.Now > ExpiresTime;

    [JsonIgnore]
    public bool IsRefreshTokenExpire => DateTime.Now > RefreshTokenExpire;

    public static async Task<AccessTokenInfo?> ReadFromFileAsync(string FileName, CancellationToken Cancel = default)
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
        await using var cancel_registration = Cancel.Register(o => File.Delete((string)o!), FileName);
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
