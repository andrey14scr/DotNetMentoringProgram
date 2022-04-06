using System.Configuration;
using System.Reflection;
using ConfigurationComponent.Common;

namespace ConfigurationComponent.ConfigurationManagerProvider;

public class ConfigurationManagerProvider<T> : IConfigurationComponent<T> where T : new()
{
    private const string InitFileContent =
        "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
        "<configuration>\n" +
        "<appSettings></appSettings>\n" +
        "</configuration>";

    public void Save(T settings)
    {
        var settingsType = typeof(T);
        var props = settingsType.GetProperties();

        foreach (var prop in props)
        {
            if (prop.GetCustomAttribute(typeof(ConfigurationItemAttribute)) is not ConfigurationItemAttribute configurationItemAttribute)
            {
                continue;
            }

            if (configurationItemAttribute.ProviderType != Providers.ConfigurationManager)
            {
                continue;
            }

            var value = prop.GetValue(settings);
            if (value is null)
            {
                continue;
            }
            var key = prop.Name;
            var fileName = Path.Combine(Directory.GetCurrentDirectory(), configurationItemAttribute.SettingName);

            if (!File.Exists(fileName))
            {
                File.WriteAllText(fileName, InitFileContent);
            }
            
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", fileName);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings[key].Value = value.ToString();
            }
            else
            {
                config.AppSettings.Settings.Add(key, value.ToString());
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }

    public T Load()
    {
        var settings = new T();
        var settingsType = typeof(T);
        var props = settingsType.GetProperties();

        foreach (var prop in props)
        {
            if (prop.GetCustomAttribute(typeof(ConfigurationItemAttribute)) is not ConfigurationItemAttribute configurationItemAttribute)
            {
                continue;
            }

            if (configurationItemAttribute.ProviderType != Providers.ConfigurationManager)
            {
                continue;
            }

            var configFile = configurationItemAttribute.SettingName;
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", configFile);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var value = config.AppSettings.Settings[prop.Name]?.Value;

            if (value is null)
            {
                prop.SetValue(settings, null);
            }
            else
            {
                var realType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (realType == typeof(string))
                {
                    prop.SetValue(settings, value);
                }
                else
                {
                    var methodInfo = realType.GetMethods().First(m => m.Name == "Parse");
                    var classInstance = Activator.CreateInstance(realType, null);
                    var obj = methodInfo.Invoke(classInstance, new object[] { value });
                    prop.SetValue(settings, obj);
                }
            }
        }

        return settings;
    }
}