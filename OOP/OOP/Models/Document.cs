namespace FileCabinet.Models;

public abstract class Document
{
    public string Title { get; set; }
    public DateTime DatePublished { get; set; }
}