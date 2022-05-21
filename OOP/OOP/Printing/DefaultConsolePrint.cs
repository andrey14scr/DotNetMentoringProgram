namespace FileCabinet.Printing;

public class DefaultConsolePrint : IConsolePrint
{
    private readonly string _separator;

    public DefaultConsolePrint(string separator)
    {
        _separator = separator;
    }

    public void Print(IEnumerable<string> cards)
    {
        foreach (var file in cards)
        {
            Console.Write(file);
            Console.Write(_separator);
        }
    }
}