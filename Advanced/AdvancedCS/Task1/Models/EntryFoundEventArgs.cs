namespace HomeTask.Models;

public class EntryFoundEventArgs : MessageEventArgs
{
    public EntryInfo EntryInfo { get; }

    public EntryFoundEventArgs(string message, EntryInfo entryInfo) : base(message)
    {
        EntryInfo = entryInfo;
    }
}