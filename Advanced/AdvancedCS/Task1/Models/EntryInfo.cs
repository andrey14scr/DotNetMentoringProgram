namespace HomeTask.Models;

public class EntryInfo
{
    public DateTime CreationDate { get; }

    public string Name { get; }

    public EntryInfo(string name)
    {
        Name = name;
        CreationDate = File.GetCreationTime(name);
    }

    public string ToShortFormat()
    {
        return $"{CreationDate}: {Name}";
    }

    public override bool Equals(object obj)
    {
        var entryInfo = obj as EntryInfo;
        if (entryInfo is null)
            return false;

        return Name.Equals(entryInfo.Name) && CreationDate.Equals(entryInfo.CreationDate);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, CreationDate);
    }

}