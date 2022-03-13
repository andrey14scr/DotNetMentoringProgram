namespace HomeTask.Models;

public record EntryInfo(string Name, DateTime CreationDate)
{
    public string ToShortFormat()
    {
        return $"{CreationDate}: {Name}";
    }
}