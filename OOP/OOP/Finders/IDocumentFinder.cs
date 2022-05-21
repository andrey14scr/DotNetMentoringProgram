using FileCabinet.Models;

namespace FileCabinet.Finders;

public interface IDocumentFinder
{
    List<Document> FindByNumber(int number);
}