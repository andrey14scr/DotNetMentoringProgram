using FileCabinet.Models;
using FileCabinet.ParsersChainFactory;

namespace FileCabinet.Finders;

public class FileSystemDocFinder : IDocumentFinder
{
    private readonly string _root;

    public Func<string, (string Type, int Number)> GetFileInfo { get; set; }

    public FileSystemDocFinder(string root, Func<string, (string Type, int Number)> getFileInfo)
    {
        if (!Directory.Exists(root))
        {
            throw new ArgumentException("Root should be an existing folder.");
        }

        _root = root;
        GetFileInfo = getFileInfo;
    }

    public List<Document> FindByNumber(int number)
    {
        var docs = new List<Document>();

        var creator = new ParserChainFactory();
        var fileParsers = creator.GetParserChain(GetFileInfo, number);

        foreach (var file in Directory.GetFiles(_root))
        {
            var doc = fileParsers.GetDocument(file);
            if (doc is not null)
            {
                docs.Add(doc);
            }
        }

        return docs;
    }
}