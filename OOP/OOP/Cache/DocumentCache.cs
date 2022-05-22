using FileCabinet.Models;

namespace FileCabinet.Cache;

public class DocumentCache<TKey, TValue> : IDocumentCache<TKey, TValue> where TValue : Document where TKey : notnull
{
    private readonly Dictionary<TKey, (TValue Document, DateTime? Expiration)> _cache;

    public int CashTime { get; set; }

    public DocumentCache(int cashTime)
    {
        CashTime = cashTime;
        _cache = new Dictionary<TKey, (TValue Document, DateTime? Expiration)>();
    }

    public bool TryGet(TKey key, out TValue document)
    {
        document = null;

        if (_cache.TryGetValue(key, out var result))
        {
            if (result.Expiration is not null && result.Expiration <= DateTime.Now)
            {
                document = result.Document;
                return true;
            }

            _cache.Remove(key);
        }

        return false;
    }

    public void Add(TKey key, TValue document)
    {
        if (CashTime == 0)
        {
            return;
        }
        _cache.Add(key, (document, CashTime < 0 ? null : DateTime.Now.AddMinutes(CashTime)));
    }
}