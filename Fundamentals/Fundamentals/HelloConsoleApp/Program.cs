using HelloLibrary;

namespace HelloConsoleApp;

class Hello
{
    static void Main(string[] args)
    {
        if (args.Length != 2 || args[0] != "-username")
        {
            Console.WriteLine();
            return;
        }

        var username = args[1];
        Console.WriteLine($"Hello {username}");

        var helloService = new HelloService();
        var greeting = helloService.GetHelloString(username);
        Console.WriteLine(greeting);
    }
}
