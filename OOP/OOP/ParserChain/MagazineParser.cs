using System.Text.Json;
using FileCabinet.Cache;
using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public class MagazineParser : FileParser
{
    private readonly IDocumentCache<string, Magazine> _cache;

    public int CashTime
    {
        get => _cache.CashTime;
        set => _cache.CashTime = value;
    }

    public MagazineParser(string bookType, Func<string, (string Type, int Number)> getFileInfo, int number, IDocumentCache<string, Magazine> cache) : base(bookType, getFileInfo, number)
    {
        _cache = cache;
    }

    public override Document GetDocument(string file)
    {
        if (_cache.TryGet(file, out var result))
        {
            return result;
        }

        var info = GetFileInfo(file);

        if (info.Type.Equals(_type) && _number == info.Number)
        {
            var magazine = JsonSerializer.Deserialize<Magazine>(File.ReadAllText(file));
            if (magazine is not null)
            {
                _cache.Add(file, magazine);
            }
            return magazine;
        }

        return base.GetDocument(file);
    }
}