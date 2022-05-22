namespace FileCabinet.Cache;

public interface IDocumentCache<TKey, TValue> where TKey : notnull
{
    public int CashTime { get; set; }
    public bool TryGet(TKey key, out TValue document);
    public void Add(TKey key, TValue document);
}