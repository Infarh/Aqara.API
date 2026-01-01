using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aqara.API;

/// <summary>Информация о токене авторизации</summary>
/// <param name="AccessToken">Токен авторизации</param>
/// <param name="RefreshToken">Токен обновления токена авторизации (действителен 30 дней)</param>
/// <param name="Expires">Интервал времени в секундах валидности токена авторизации</param>
/// <param name="OpenId">Уникальный идентификатор пользователя</param>
public record AccessTokenInfo(string AccessToken, string RefreshToken, int Expires, string OpenId)
{
    /// <summary>Время создания токена</summary>
    public DateTime CreationTime { get; init; } = DateTime.Now;

    /// <summary>Вычисленное время истечения действия токена</summary>
    [JsonIgnore]
    public DateTime ExpiresTime => CreationTime.AddSeconds(Expires);

    /// <summary>Время истечения действия refresh токена (через 30 дней после создания)</summary>
    [JsonIgnore]
    public DateTime RefreshTokenExpire => CreationTime.AddDays(30);

    /// <summary>Признак того что access токен истёк</summary>
    [JsonIgnore]
    public bool IsExpire => DateTime.Now > ExpiresTime;

    /// <summary>Признак того что refresh токен истёк</summary>
    [JsonIgnore]
    public bool IsRefreshTokenExpire => DateTime.Now > RefreshTokenExpire;

    /// <summary>Асинхронно прочитать данные токена из файла</summary>
    /// <param name="FileName">Путь к файлу с данными токена</param>
    /// <param name="Cancel">Токен отмены операции</param>
    /// <returns>Десериализованный объект информации о токене или null</returns>
    /// <exception cref="FileNotFoundException">Если файл не найден</exception>
    /// <exception cref="InvalidOperationException">Если не удалось десериализовать данные токена</exception>
    /// <exception cref="OperationCanceledException">Если операция была отменена</exception>
    public static async Task<AccessTokenInfo?> ReadFromFileAsync(string FileName, CancellationToken Cancel = default)
    {
        FileNotFoundException.ThrowIfNotExists(FileName, "Файл данных токена не найден");

        await using var reader = File.OpenRead(FileName);
        return await JsonSerializer
               .DeserializeAsync<AccessTokenInfo>(reader, cancellationToken: Cancel)
               .ConfigureAwait(false)
            ?? throw new InvalidOperationException("Не удалось прочитать данные токена");
    }

    /// <summary>Синхронно прочитать данные токена из файла</summary>
    /// <param name="FileName">Путь к файлу с данными токена</param>
    /// <returns>Десериализованный объект информации о токене</returns>
    /// <exception cref="FileNotFoundException">Если файл не найден</exception>
    /// <exception cref="InvalidOperationException">Если не удалось десериализовать данные токена</exception>
    public static AccessTokenInfo ReadFromFile(string FileName)
    {
        FileNotFoundException.ThrowIfNotExists(FileName, "Файл данных токена не найден");

        using var reader = File.OpenRead(FileName);
        return JsonSerializer
               .Deserialize<AccessTokenInfo>(reader)
            ?? throw new InvalidOperationException("Не удалось прочитать данные токена");
    }

    private readonly Lazy<JsonSerializerOptions> __IdentedOpts = new(() => new JsonSerializerOptions { WriteIndented = true });

    /// <summary>Асинхронно сохранить данные токена в файл</summary>
    /// <param name="FileName">Путь к файлу для записи данных токена</param>
    /// <param name="Cancel">Токен отмены операции</param>
    /// <returns>Задача, представляющая асинхронную операцию записи</returns>
    /// <exception cref="OperationCanceledException">Если операция была отменена</exception>
    public async Task SaveToFileAsync(string FileName, CancellationToken Cancel = default)
    {
        Cancel.ThrowIfCancellationRequested();

        try
        {
            await using var writer = FileEx.Create(
                FileName,
                bufferSize: 4096,
                useAsync: true
            );

            await JsonSerializer
               .SerializeAsync(writer, this, __IdentedOpts.Value, Cancel)
               .ConfigureAwait(false);

            await writer.FlushAsync(Cancel).ConfigureAwait(false);
        }
        catch (OperationCanceledException)
        {
            if (File.Exists(FileName))
                try
                {
                    File.Delete(FileName);
                }
                catch { /* игнорируем ошибки удаления */ }
            throw;
        }
    }

    /// <summary>Синхронно сохранить данные токена в файл</summary>
    /// <param name="FileName">Путь к файлу для записи данных токена</param>
    public void SaveToFile(string FileName)
    {
        using var writer = File.Create(FileName);
        JsonSerializer.Serialize(writer, this, __IdentedOpts.Value);
    }
}
