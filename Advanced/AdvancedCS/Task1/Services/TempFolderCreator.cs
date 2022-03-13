namespace HomeTask.Services;

public class TempFolderCreator
{
    public string CreateForTest(string root, int count)
    {
        var filesCount = 1;
        var foldersCount = 1;

        if (Directory.Exists(root))
        {
            root += "_Copy";
        }

        Directory.CreateDirectory(root);
        var rand = new Random();

        for (var i = 0; i < count; i++)
        {
            if (rand.Next() % 2 == 0)
            {
                CreateTestFile(Path.Combine(root, $"file{filesCount++}.txt"));
            }
            else
            {
                CreateTestFolder(Path.Combine(root, $"folder{foldersCount++}"));
            }
        }

        return root;
    }

    private static void CreateTestFile(string path)
    {
        Thread.Sleep(1000);
        File.Create(path).Close();
        Console.WriteLine($"File {path} created.");
    }

    private static void CreateTestFolder(string path)
    {
        Thread.Sleep(1000);
        Directory.CreateDirectory(path);
        Console.WriteLine($"Folder {path} created.");
    }
}