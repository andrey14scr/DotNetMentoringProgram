using CustomBinarySerialization;
using CustomBinarySerialization.Properties;

var figure = new Figure("Triangle", 12.5);

var fs = new FigureSerializer();
fs.Serialize(figure, Resources.DataFile);
var data = fs.Deserialize(Resources.DataFile);

Console.WriteLine(data.Name);
Console.WriteLine(data.Square);