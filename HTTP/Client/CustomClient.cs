﻿namespace Client;

public class CustomClient
{
    private readonly HttpClient _httpClient;

    public CustomClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task GetName(string name)
    {
        var responseBody = await _httpClient.GetStringAsync($"http://localhost:8888/MyName/{name}");
        Console.WriteLine(responseBody);
    }
}