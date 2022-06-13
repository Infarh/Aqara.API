using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Aqara.API.Infrastructure;

public static class HttpClientExtensions
{
    public static async Task<TResult?> PostFromJsonAsync<TResult, TValue>(
        this HttpClient client,
        string uri,
        TResult value,
        CancellationToken Cancel = default)
    {
        var response = await client.PostAsJsonAsync(uri, value, cancellationToken: Cancel).GetJsonResult<TResult>(Cancel).ConfigureAwait(false);

        return response;
    }

    public static async Task<TResult?> GetJsonResult<TResult>(this Task<HttpResponseMessage> ResponseTask, CancellationToken Cancel = default)
    {
        var response = await ResponseTask.ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.NoContent)
            return default;

        var json_string = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync(Cancel);

        var result = JsonSerializer.Deserialize<TResult>(json_string);

        //var result = await response
        //   .EnsureSuccessStatusCode()
        //   .Content
        //   .ReadFromJsonAsync<TResult>(cancellationToken: Cancel);

        return result
            ?? throw new InvalidOperationException($"Не удалось прочитать данные ответа в формате {typeof(TResult)}");
    }
}
