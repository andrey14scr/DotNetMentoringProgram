using System.Text.Json;
using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public class LocalizedBookParser : FileParser
{
    public LocalizedBookParser(string localizedBookType, Func<string, (string Type, int Number)> getFileInfo, int number) : base(localizedBookType, getFileInfo, number)
    {
    }

    public override Document GetDocument(string file)
    {
        var info = GetFileInfo(file);

        if (info.Type.Equals(_type) && _number == info.Number)
        {
            return JsonSerializer.Deserialize<LocalizedBook>(File.ReadAllText(file));
        }

        return base.GetDocument(file);
    }
}