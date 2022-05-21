namespace FileCabinet.Models;

public class LocalizedBook : Book
{
    public string Authors { get; set; }
    public string LocalCountry { get; set; }
    public string LocalPublisher { get; set; }
}