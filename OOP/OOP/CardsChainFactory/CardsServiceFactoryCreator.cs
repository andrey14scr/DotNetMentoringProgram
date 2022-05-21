using FileCabinet.CardInfoChain;

namespace FileCabinet.CardsChainFactory;

public abstract class CardsServiceFactoryCreator
{
    public abstract ICardsServiceChain CardsServiceChain();

    public ICardInfoService GetCardsServiceChain()
    {
        return CardsServiceChain().GetDocumentsInfoServiceChain();
    }
}