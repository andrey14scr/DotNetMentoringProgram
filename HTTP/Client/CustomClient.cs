using System.Net;
using Client.Properties;

namespace Client;

public class CustomClient
{
    private readonly string _baseAddress;
    private readonly HttpClient _httpClient;

    public CustomClient(string baseAddress)
    {
        _baseAddress = baseAddress;
        _httpClient = new HttpClient();
    }

    public async Task GetName(string name)
    {
        var response = await _httpClient.GetAsync($"{_baseAddress}/{Resources.MyNameUrl}/{name}");
        await PrintInfo(response);
    }

    public async Task MyNameByHeader(string name)
    {
        _httpClient.DefaultRequestHeaders.Add("X-MyName", name);
        var response = await _httpClient.GetAsync($"{_baseAddress}/{Resources.MyNameByHeaderUrl}");
        _httpClient.DefaultRequestHeaders.Remove("X-MyName");
        await PrintInfo(response);
    }

    public async Task MyNameByCookies(string name)
    {
        var cookieContainer = new CookieContainer();
        using (var handler = new HttpClientHandler { CookieContainer = cookieContainer })
        {
            using (var client = new HttpClient(handler) { BaseAddress = new Uri(_baseAddress) })
            {
                cookieContainer.Add(new Uri(_baseAddress), new Cookie("MyName", name));
                var response = await client.GetAsync($"{_baseAddress}/{Resources.MyNameByCookiesUrl}");
                await PrintInfo(response);
            }
        }
    }

    public async Task Get(string method)
    {
        var response = await _httpClient.GetAsync($"{_baseAddress}/{method}");
        await PrintInfo(response);
    }

    private static async Task PrintInfo(HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Request: " + response.RequestMessage.RequestUri);
        Console.WriteLine("Response: " + responseString);
        Console.WriteLine("Code: " + response.StatusCode);
        Console.WriteLine();
    }
}