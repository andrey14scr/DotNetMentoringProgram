using FileCabinet.ParserChain;

namespace FileCabinet.ParsersChainFactory;

public abstract class ParsersFactoryCreator
{
    public abstract IParserChain ParserChain();

    public IFileParser GetParserChain(Func<string, (string Type, int Number)> getFileInfo, int number)
    {
        return ParserChain().GetParserChain(getFileInfo, number);
    }
}