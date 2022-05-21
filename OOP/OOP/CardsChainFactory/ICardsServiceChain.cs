using FileCabinet.CardInfoChain;

namespace FileCabinet.CardsChainFactory;

public interface ICardsServiceChain
{
    ICardInfoService GetDocumentsInfoServiceChain();
}