using System.Collections;
using HomeTask.Models;

namespace HomeTask.Services;

public class FileSystemVisitor : IEnumerable<EntryInfo>
{
    private readonly Comparison<EntryInfo> _algorithm;
    private readonly string _path;

    public FileSystemVisitor(string path)
        : this(path, (s1, s2) => string.Compare(s1.Name, s2.Name, StringComparison.Ordinal))
    {
    }

    public FileSystemVisitor(string path, Comparison<EntryInfo> algorithm)
    {
        _path = path;
        _algorithm = algorithm;
    }

    public IEnumerator<EntryInfo> GetEnumerator()
    {
        var entries = new List<EntryInfo>();

        var folderContent = Directory.GetFileSystemEntries(_path, "*.*", SearchOption.TopDirectoryOnly);
        foreach (var entry in folderContent)
        {
            entries.Add(new EntryInfo(entry, File.GetCreationTime(entry)));
        }

        entries.Sort(_algorithm);

        foreach (var entry in entries)
        {
            yield return entry;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}