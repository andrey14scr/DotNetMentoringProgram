using FileCabinet.ParserChain;

namespace FileCabinet.ParsersChainFactory;

public class ParserChain : IParserChain
{
    public IFileParser GetParserChain(Func<string, (string Type, int Number)> getFileInfo, int number)
    {
        var patent = new PatentParser("patent", getFileInfo, number);
        var book = new BookParser("book", getFileInfo, number);
        var locBook = new LocalizedBookParser("localized book", getFileInfo, number);

        book.SetNext(patent).SetNext(locBook);

        return book;
    }
}