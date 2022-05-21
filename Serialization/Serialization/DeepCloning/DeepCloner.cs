#pragma warning disable SYSLIB0011
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepCloning;

public static class DeepCloner
{
    public static T Copy<T>(object objSource)
    {
        using (var stream = new MemoryStream())
        {
            var bf = new BinaryFormatter();
            bf.Serialize(stream, objSource);
            stream.Position = 0;
            return (T)bf.Deserialize(stream);
        }
    }
}