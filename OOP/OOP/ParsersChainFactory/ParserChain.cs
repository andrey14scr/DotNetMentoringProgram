using FileCabinet.Cache;
using FileCabinet.Models;
using FileCabinet.ParserChain;
using FileCabinet.Properties;

namespace FileCabinet.ParsersChainFactory;

public class ParserChain : IParserChain
{
    public IFileParser GetParserChain(Func<string, (string Type, int Number)> getFileInfo, int number)
    {
        var patent = new PatentParser(Resources.Patent, getFileInfo, number, new DocumentCache<string, Patent>(0));
        var book = new BookParser(Resources.Book, getFileInfo, number, new DocumentCache<string, Book>(-1));
        var locBook = new LocalizedBookParser(Resources.LocalizedBook, getFileInfo, number, new DocumentCache<string, LocalizedBook>(0));
        var magazine = new MagazineParser(Resources.Magazine, getFileInfo, number, new DocumentCache<string, Magazine>(10));

        book.SetNext(patent).SetNext(locBook).SetNext(magazine);

        return book;
    }
}