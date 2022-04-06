namespace ConfigurationComponent.Common;

public interface IConfigurationComponent<T>
{
    void Save(T settings);
    T Load();
}