namespace FileCabinet.ParsersChainFactory;

public class ParserChainFactory : ParsersFactoryCreator
{
    public override IParserChain ParserChain()
    {
        return new ParserChain();
    }
}