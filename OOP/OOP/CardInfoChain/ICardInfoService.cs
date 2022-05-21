using FileCabinet.Models;

namespace FileCabinet.CardInfoChain;

public interface ICardInfoService
{
    ICardInfoService SetNext(ICardInfoService infoService);

    string GetInfo(Document document);
}