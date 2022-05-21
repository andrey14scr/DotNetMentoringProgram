using FileCabinet.Finders;
using FileCabinet.Models;
using FileCabinet.Output;

namespace FileCabinet;

public class FileCabinet
{
    public IDocumentFinder DocumentFinder { get; set; }
    public IOutputService OutputService { get; set; }

    public FileCabinet(IDocumentFinder documentFinder, IOutputService outputService)
    {
        DocumentFinder = documentFinder;
        OutputService = outputService;
    }

    public IEnumerable<Document> FindByNumber(int number)
    {
        return DocumentFinder.FindByNumber(number);
    }

    public void GetInfo(IEnumerable<Document> documents)
    {
        OutputService.Out(documents);
    }
}