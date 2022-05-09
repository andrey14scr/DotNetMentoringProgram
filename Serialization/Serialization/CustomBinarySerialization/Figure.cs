using System.Runtime.Serialization;

namespace CustomBinarySerialization;

[Serializable]
public class Figure : ISerializable
{
    public string Name { get; set; }
    public double Square { get; set; }

    public Figure()
    {
        
    }

    public Figure(string name, double square)
    {
        Name = name; 
        Square = square;
    }

    public Figure(SerializationInfo info, StreamingContext context)
    {
        Name = (string)info.GetValue("Name", typeof(string));
        Square = (double)info.GetValue("Square", typeof(double));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Name", Name);
        info.AddValue("Square", Square);
    }
}