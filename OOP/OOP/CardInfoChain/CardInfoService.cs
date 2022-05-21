using FileCabinet.Models;

namespace FileCabinet.CardInfoChain;

public abstract class CardInfoService : ICardInfoService
{
    private ICardInfoService? _nextHandler;

    public ICardInfoService SetNext(ICardInfoService infoService)
    {
        _nextHandler = infoService;
        return infoService;
    }

    public virtual string GetInfo(Document document)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        return _nextHandler?.GetInfo(document) ?? string.Empty;
    }
}