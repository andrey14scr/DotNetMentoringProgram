using System.Runtime.Serialization.Formatters.Binary;

namespace CustomBinarySerialization;

public class FigureSerializer
{
    public void Serialize(Figure figure, string fileName)
    {
        using (var stream = new FileStream(fileName, FileMode.Create))
        {
            var bf = new BinaryFormatter();
            bf.Serialize(stream, figure);
        }
    }

    public Figure Deserialize(string fileName)
    {
        Figure figure;
        using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
        {
            var bf = new BinaryFormatter();
            figure = (Figure)bf.Deserialize(fs);
        }

        return figure;
    }
}