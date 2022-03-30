using System.Reflection;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Task1.Properties;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Task1;

public class ConfigurationComponentBase<T> where T : new()
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

            var value = prop.GetValue(settings);
            if (value is null)
            {
                continue;
            }
            var key = prop.Name;
            var fileName = configurationItemAttribute.SettingName;

            switch (configurationItemAttribute.ProviderType)
            {
                case Providers.FileConfiguration:
                    var json = File.ReadAllText(fileName);
                    var obj = JObject.Parse(json);
                    var jSettings = (JObject)obj[settingsType.Name]!;
                    jSettings[key] = value.ToString();
                    File.WriteAllText(fileName, obj.ToString());
                    break;
                case Providers.ConfigurationManager:
                    var xDoc = XDocument.Load(fileName);
                    var root = xDoc.Root!;
                    var node = root.Element(Resources.AppSettingsRoot)!;
                    var toSet = node.Elements(Resources.AddNode).FirstOrDefault(e => e.FirstAttribute?.Value == key);
                    if (toSet != null)
                    {
                        toSet.Attribute(Resources.ValyeAttrivute)!.Value = value.ToString()!;
                    }
                    else
                    {
                        node.Add(new XElement(Resources.AddNode, new XAttribute(Resources.KeyAttribute, key), new XAttribute(Resources.ValyeAttrivute, value)));
                    }
                    xDoc.Save(fileName);
                    break;
            }
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

            var fileName = configurationItemAttribute.SettingName;
            var res = string.Empty;

            switch (configurationItemAttribute.ProviderType)
            {
                case Providers.FileConfiguration:
                    var config = new ConfigurationBuilder()
                        .AddJsonFile(fileName)
                        .AddEnvironmentVariables()
                        .Build();
                    res = config.GetRequiredSection(settingsType.Name).GetSection(prop.Name).Value;
                    break;
                case Providers.ConfigurationManager:
                    res = ConfigurationManager.AppSettings[prop.Name];
                    break;
            }

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