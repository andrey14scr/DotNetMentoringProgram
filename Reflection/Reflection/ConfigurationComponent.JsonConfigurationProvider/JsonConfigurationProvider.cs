using System.Reflection;
using ConfigurationComponent.Common;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace ConfigurationComponent.JsonConfigurationProvider
{
    public class JsonConfigurationProvider<T> : IConfigurationComponent<T> where T : new()
    {
        public void Save(T settings)
        {
            var settingsType = typeof(T);
            var props = settingsType.GetProperties();

            foreach (var prop in props)
            {
                if (prop.GetCustomAttribute(typeof(ConfigurationItemAttribute)) is not ConfigurationItemAttribute configurationItemAttribute)
                {
                    throw new NullReferenceException("Attribute of one of settings was null.");
                }

                if (configurationItemAttribute.ProviderType != Providers.FileConfiguration)
                {
                    continue;
                }

                var value = prop.GetValue(settings);
                if (value is null)
                {
                    continue;
                }
                var key = prop.Name;
                var fileName = configurationItemAttribute.SettingName;

                var json = File.ReadAllText(fileName);
                var obj = JObject.Parse(json);
                var jSettings = (JObject)obj[settingsType.Name]!;
                jSettings[key] = value.ToString();
                File.WriteAllText(fileName, obj.ToString());
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
                    throw new NullReferenceException("Attribute of one of settings was null.");
                }

                if (configurationItemAttribute.ProviderType != Providers.FileConfiguration)
                {
                    continue;
                }

                var fileName = configurationItemAttribute.SettingName;

                var config = new ConfigurationBuilder()
                    .AddJsonFile(fileName)
                    .AddEnvironmentVariables()
                    .Build();
                var res = config.GetRequiredSection(settingsType.Name).GetSection(prop.Name).Value;

                if (res is null)
                {
                    prop.SetValue(settings, null);
                }
                else
                {
                    var realType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    if (realType == typeof(string))
                    {
                        prop.SetValue(settings, res);
                    }
                    else
                    {
                        var methodInfo = realType.GetMethods().First(m => m.Name == "Parse");
                        var classInstance = Activator.CreateInstance(realType, null);
                        var obj = methodInfo.Invoke(classInstance, new object[] { res });
                        prop.SetValue(settings, obj);
                    }
                }
            }

            return settings;
        }
    }
}