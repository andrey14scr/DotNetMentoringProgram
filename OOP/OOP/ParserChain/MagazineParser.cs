using System.Text.Json;
using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public class MagazineParser : FileParser
{
    public MagazineParser(string bookType, Func<string, (string Type, int Number)> getFileInfo, int number) : base(bookType, getFileInfo, number)
    {
    }

    public override Document GetDocument(string file)
    {
        var info = GetFileInfo(file);

        if (info.Type.Equals(_type) && _number == info.Number)
        {
            return JsonSerializer.Deserialize<Magazine>(File.ReadAllText(file));
        }

        return base.GetDocument(file);
    }
}