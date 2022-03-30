namespace Task1;

public class Settings
{

    [ConfigurationItem("appsettings.json", Providers.FileConfiguration)]
    public int? MaxConnections { get; set; }

    [ConfigurationItem("Task1.dll.config", Providers.ConfigurationManager)]
    public float? AverageSpeed { get; set; }

    [ConfigurationItem("Task1.dll.config", Providers.ConfigurationManager)]
    public string? Name { get; set; }

    [ConfigurationItem("appsettings.json", Providers.FileConfiguration)]
    public TimeSpan? Usage { get; set; }

    public override string ToString()
    {
        return $"MaxConnections: {MaxConnections}, AverageSpeed: {AverageSpeed}, Name: {Name}, Usage: {Usage}";
    }
}