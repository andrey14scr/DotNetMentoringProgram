using FileCabinet.Models;

namespace FileCabinet.Output;

public interface IOutputService
{
    void Out(IEnumerable<Document> documents);
}