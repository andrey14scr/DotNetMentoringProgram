using System.Reflection;
using ConfigurationComponent.Common;

namespace Task1;

class Program
{
    static void Main(string[] args)
    {
        var currentDir = Directory.GetCurrentDirectory();
        var providers = new List<IConfigurationComponent<Settings>>();

        foreach (var file in Directory.GetFiles(@".\Plugins", "*.dll"))
        {
            var asm = Assembly.LoadFrom(currentDir + file);
            foreach (var type in asm.GetTypes())
            {
                if (type.GetInterfaces().Any(x => x.IsGenericType && 
                                                  x.GetGenericTypeDefinition() == typeof(IConfigurationComponent<>)))
                {
                    var target = type.MakeGenericType(new []{ typeof(Settings) });
                    var provider = Activator.CreateInstance(target) as IConfigurationComponent<Settings>;
                    providers.Add(provider);
                }
            }
        }

        var settings = new Settings
        {
            AverageSpeed = 2222,
            Name = "2222",
            MaxConnections = 2222,
            Usage = new TimeSpan(2222),
        };

        Console.WriteLine(settings.ToString());
        Console.WriteLine();

        foreach (var provider in providers)
        {
            provider.Save(settings);
            var loadedSettings = provider.Load();
            Console.WriteLine(loadedSettings);
        }
    }
}