using System.Text.Json;
using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public class PatentParser : FileParser
{
    public PatentParser(string patentType, Func<string, (string Type, int Number)> getFileInfo, int number) : base(patentType, getFileInfo, number)
    {
    }

    public override Document GetDocument(string file)
    {
        var info = GetFileInfo(file);

        if (info.Type.Equals(_type) && _number == info.Number)
        {
            return JsonSerializer.Deserialize<Patent>(File.ReadAllText(file));
        }

        return base.GetDocument(file);
    }
}