using FileCabinet.ParserChain;

namespace FileCabinet.ParsersChainFactory;

public interface IParserChain
{
    IFileParser GetParserChain(Func<string, (string Type, int Number)> getFileInfo, int number);
}