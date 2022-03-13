namespace HomeTask.Services;

public class TempFolderCreator
{
    public void CreateForTest(string root)
    {
        var index = 0;
        while (Directory.Exists(root))
        {
            index++;
        }

        if (index != 0)
        {
            root += index;
        }

        Directory.CreateDirectory(root);

        CreateTestFile(Path.Combine(root, "file1.txt"));
        CreateTestFile(Path.Combine(root, "file2.txt"));
        CreateTestFolder(Path.Combine(root, "folder1"));
        CreateTestFile(Path.Combine(root, "file3.txt"));
        CreateTestFolder(Path.Combine(root, "folder2"));
        CreateTestFile(Path.Combine(root, "file4.txt"));
        CreateTestFile(Path.Combine(root, "file5.txt"));
        CreateTestFolder(Path.Combine(root, "folder3"));
        CreateTestFolder(Path.Combine(root, "folder4"));
        CreateTestFolder(Path.Combine(root, "folder5"));
    }

    private static void CreateTestFile(string path)
    {
        Thread.Sleep(1000);
        File.Create(path);
        Console.WriteLine($"File {path} created.");
    }

    private static void CreateTestFolder(string path)
    {
        Thread.Sleep(1000);
        Directory.CreateDirectory(path);
        Console.WriteLine($"Folder {path} created.");
    }
}