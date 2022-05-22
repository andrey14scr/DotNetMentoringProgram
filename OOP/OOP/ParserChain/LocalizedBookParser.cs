using System.Text.Json;
using FileCabinet.Cache;
using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public class LocalizedBookParser : FileParser
{
    private readonly IDocumentCache<string, LocalizedBook> _cache;

    public int CashTime
    {
        get => _cache.CashTime;
        set => _cache.CashTime = value;
    }

    public LocalizedBookParser(string type, Func<string, (string Type, int Number)> getFileInfo, int number, IDocumentCache<string, LocalizedBook> cache) : base(type, getFileInfo, number)
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
            var book = JsonSerializer.Deserialize<LocalizedBook>(File.ReadAllText(file));
            if (book is not null)
            {
                _cache.Add(file, book);
            }
            return book;
        }

        return base.GetDocument(file);
    }
}