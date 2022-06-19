using Client.Properties;

namespace Client;

public class CustomClient
{
    private readonly HttpClient _httpClient;

    public CustomClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task GetName(string name)
    {
        var response = await _httpClient.GetAsync($"http://localhost:8888/{Resources.MyNameUrl}/{name}");
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Response: " + responseString);
        Console.WriteLine("Code: " + response.StatusCode);
    }

    public async Task Get(string method)
    {
        var response = await _httpClient.GetAsync($"http://localhost:8888/{method}");
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Response: " + responseString);
        Console.WriteLine("Code: " + response.StatusCode);
    }
}