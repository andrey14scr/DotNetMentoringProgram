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
        Console.WriteLine();
    }

    public async Task MyNameByHeader(string name)
    {
        _httpClient.DefaultRequestHeaders.Add("X-MyName", name);
        var response = await _httpClient.GetAsync($"http://localhost:8888/{Resources.MyNameByHeaderUrl}");
        _httpClient.DefaultRequestHeaders.Remove("X-MyName");
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Response: " + responseString);
        Console.WriteLine("Code: " + response.StatusCode);
        Console.WriteLine();
    }

    public async Task Get(string method)
    {
        var response = await _httpClient.GetAsync($"http://localhost:8888/{method}");
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Response: " + responseString);
        Console.WriteLine("Code: " + response.StatusCode);
        Console.WriteLine();
    }
}