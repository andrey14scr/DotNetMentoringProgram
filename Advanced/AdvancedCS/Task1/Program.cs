using HomeTask.Models;
using HomeTask.Services;

namespace HomeTask;

public class Program
{
    static void Main(string[] args)
    {
        const string root = "c:\\RootDirectoryToTest";
        if (!Directory.Exists(root))
        {
            new TempFolderCreator().CreateForTest(root);
        }

        var visitor = new FileSystemVisitor(root, (e1, e2) => string.Compare(e1.Name, e2.Name, StringComparison.Ordinal));

        visitor.Started += OnStarted;
        visitor.Finished += OnFinished;

        visitor.FileFound += OnFileFound;
        visitor.DirectoryFound += OnDirectoryFound;
        visitor.FilteredFileFound += OnFilteredFileFound;
        visitor.FilteredDirectoryFound += OnFilteredDirectoryFound;

        foreach (var item in visitor)
        {
            Console.WriteLine(item.ToShortFormat());
        }
    }

    private static bool OnFilteredDirectoryFound(object sender, EntryFoundEventArgs e)
    {
        Console.WriteLine(e.Message);
        return false;
    }

    private static bool OnFilteredFileFound(object sender, EntryFoundEventArgs e)
    {
        Console.WriteLine(e.Message);
        return false;
    }

    private static bool OnDirectoryFound(object sender, EntryFoundEventArgs e)
    {
        Console.WriteLine(e.Message);
        return false;
    }

    private static bool OnFileFound(object sender, EntryFoundEventArgs e)
    {
        Console.WriteLine(e.Message);
        return false;
    }

    private static void OnFinished(object sender, MessageEventArgs e)
    {
        Console.WriteLine(e.Message);
    }

    private static void OnStarted(object sender, MessageEventArgs e)
    {
        Console.WriteLine(e.Message);
    }
}