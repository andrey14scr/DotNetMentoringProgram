namespace FileCabinet.Models;

public class Book : Document
{
    public string Authors { get; set; }
    public string ISBN { get; set; }
    public int NumberOfPages { get; set; }
    public string Publisher { get; set; }
}