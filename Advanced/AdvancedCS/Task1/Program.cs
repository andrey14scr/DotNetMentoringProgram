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

        foreach (var item in visitor)
        {
            Console.WriteLine(item.ToShortFormat());
        }
    }
}