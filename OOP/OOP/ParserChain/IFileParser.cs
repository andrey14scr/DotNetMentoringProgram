using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public interface IFileParser
{
    IFileParser SetNext(IFileParser fileParser);

    Document GetDocument(string file);
}