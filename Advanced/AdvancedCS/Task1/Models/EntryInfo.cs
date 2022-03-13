namespace HomeTask.Models;

public class EntryInfo
{
    public DateTime CreationDate { get; set; }

    public string Name { get; set; }

    public EntryInfo(string name)
    {
        Name = name;
        CreationDate = File.GetCreationTime(name);
    }

    public string ToShortFormat()
    {
        return $"{CreationDate}: {Name}";
    }
}