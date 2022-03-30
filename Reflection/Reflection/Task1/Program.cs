using System.ComponentModel;
using System.Configuration;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Task1;

class Program
{
    static void Main(string[] args)
    {
        var ccb = new ConfigurationComponentBase<Settings>();
        var settings = new Settings
        {
            AverageSpeed = 7777, 
            Name = "name", 
            MaxConnections = 5, 
            Usage = new TimeSpan(111),
        };

        Console.WriteLine(settings.ToString());
        Console.WriteLine();

        ccb.Save(settings);
        var loadedSettings = ccb.Load();
        Console.WriteLine(loadedSettings);
    }
}