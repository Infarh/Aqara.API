namespace Aqara.API;

public class AqaraClient
{
    private readonly HttpClient _Client;

    public AqaraClient(HttpClient Client) => _Client = Client;


}
