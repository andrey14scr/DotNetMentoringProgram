namespace FileCabinet.CardsChainFactory;

public class CardsServiceChainFactory : CardsServiceFactoryCreator
{
    public override ICardsServiceChain CardsServiceChain()
    {
        return new CardsServiceChain();
    }
}