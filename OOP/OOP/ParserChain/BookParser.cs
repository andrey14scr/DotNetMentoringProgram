using System.Text.Json;
using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public class BookParser : FileParser
{
    public BookParser(string bookType, Func<string, (string Type, int Number)> getFileInfo, int number) : base(bookType, getFileInfo, number)
    {
    }

    public override Document GetDocument(string file)
    {
        var info = GetFileInfo(file);

        if (info.Type.Equals(_type) && _number == info.Number)
        {
            return JsonSerializer.Deserialize<Book>(File.ReadAllText(file));
        }

        return base.GetDocument(file);
    }
}