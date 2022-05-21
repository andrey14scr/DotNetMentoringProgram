using FileCabinet.CardInfoChain;

namespace FileCabinet.CardsChainFactory;

public class CardsServiceChain : ICardsServiceChain
{
    public ICardInfoService GetDocumentsInfoServiceChain()
    {
        var book = new BookInfoService();
        var patent = new PatentInfoService();
        var locBook = new LocalizedBookInfoService();
        var magazine = new MagazineInfoService();

        book.SetNext(patent).SetNext(locBook).SetNext(magazine);

        return book;
    }
}