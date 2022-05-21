using FileCabinet.Models;

namespace FileCabinet.ParserChain;

public class FileParser : IFileParser
{
    private IFileParser? _nextHandler;
    protected int _number;
    protected readonly string _type;

    public Func<string, (string Type, int Number)> GetFileInfo { get; set; }

    public FileParser(string type, Func<string, (string Type, int Number)> getFileInfo, int number)
    {
        _type = type;
        _number = number;
        GetFileInfo = getFileInfo;
    }

    public IFileParser SetNext(IFileParser fileParser)
    {
        _nextHandler = fileParser;
        return fileParser;
    }

    public virtual Document GetDocument(string file)
    {
        return _nextHandler?.GetDocument(file);
    }
}