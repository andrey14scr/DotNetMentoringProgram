using System.Text.Json;
using FileCabinet.Cache;
using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public class PatentParser : FileParser
{
    private readonly IDocumentCache<string, Patent> _cache;

    public int CashTime
    {
        get => _cache.CashTime;
        set => _cache.CashTime = value;
    }

    public PatentParser(string bookType, Func<string, (string Type, int Number)> getFileInfo, int number, IDocumentCache<string, Patent> cache) : base(bookType, getFileInfo, number)
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
            var patent = JsonSerializer.Deserialize<Patent>(File.ReadAllText(file));
            if (patent is not null)
            {
                _cache.Add(file, patent);
            }
            return patent;
        }

        return base.GetDocument(file);
    }
}