namespace Task1;

[AttributeUsage(AttributeTargets.Property)]
public class ConfigurationItemAttribute : Attribute
{
    public string SettingName { get; set; }
    public Providers ProviderType { get; set; }

    public ConfigurationItemAttribute(string settingName, Providers providerType)
    {
        SettingName = settingName;
        ProviderType = providerType;
    }
}