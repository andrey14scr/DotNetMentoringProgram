namespace FileCabinet.Models;

public class Patent : Document
{
    public string Authors { get; set; }
    public Guid UniqueId { get; set; }
    public DateTime ExpirationDate { get; set; }
}