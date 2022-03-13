using System.Collections;
using HomeTask.Models;

namespace HomeTask.Services;

public class FileSystemVisitor : IEnumerable<EntryInfo>
{
    public delegate void StartHandler(object sender, MessageEventArgs e);
    public event StartHandler? Started;
    public delegate void FinishHandler(object sender, MessageEventArgs e);
    public event FinishHandler? Finished;

    public delegate bool FileFoundHandler(object sender, EntryFoundEventArgs e);
    public event FileFoundHandler? FileFound;
    public delegate bool DirectoryFoundHandler(object sender, EntryFoundEventArgs e);
    public event DirectoryFoundHandler? DirectoryFound;

    public delegate bool FilteredFileFoundHandler(object sender, EntryFoundEventArgs e);
    public event FilteredFileFoundHandler? FilteredFileFound;
    public delegate bool FilteredDirectoryFoundHandler(object sender, EntryFoundEventArgs e);
    public event FilteredDirectoryFoundHandler? FilteredDirectoryFound;

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
        Started?.Invoke(this, new MessageEventArgs("Start working."));
        
        var entries = new List<EntryInfo>();
        var folderContent = Directory.GetFileSystemEntries(_path, "*.*", SearchOption.TopDirectoryOnly);
        bool? isAbort = null;

        foreach (var entry in folderContent)
        {
            var entryInfo = new EntryInfo(entry);

            if (IsFile(entryInfo.Name))
            {
                isAbort = FileFound?.Invoke(this, new EntryFoundEventArgs($"{entryInfo.Name} found.", entryInfo));
            }
            else
            {
                isAbort = DirectoryFound?.Invoke(this, new EntryFoundEventArgs($"{entryInfo.Name} found.", entryInfo));
            }

            if (isAbort.HasValue && isAbort.Value)
            {
                break;
            }

            entries.Add(entryInfo);
        }

        entries.Sort(_algorithm);

        foreach (var entry in entries)
        {
            if (IsFile(entry.Name))
            {
                FilteredFileFound?.Invoke(this, new EntryFoundEventArgs($"Filtered {entry.Name} found.", entry));
            }
            else
            {
                FilteredDirectoryFound?.Invoke(this, new EntryFoundEventArgs($"Filtered {entry.Name} found.", entry));
            }

            yield return entry;
        }

        Finished?.Invoke(this, new MessageEventArgs("Finish working."));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static bool IsFile(string name)
    {
        return Path.GetExtension(name) != string.Empty;
    }
}