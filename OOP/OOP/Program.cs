using System.Text.Json;
using FileCabinet.CardsChainFactory;
using FileCabinet.Finders;
using FileCabinet.Models;
using FileCabinet.Output;
using FileCabinet.Printing;
using FileCabinet.Properties;

namespace FileCabinet;

internal class Program
{
    static void Main(string[] args)
    {
        var number = 2;

        var creator = new CardsServiceChainFactory();
        var infoService = creator.GetCardsServiceChain();
        var finder = new FileSystemDocFinder(Resources.RootFolder, f =>
        {
            var info = Path.GetFileNameWithoutExtension(f).Split("_#", 2);
            return (info[0], Int32.Parse(info[1]));
        });
        var print = new DefaultConsolePrint("\n=-=-=-=-=-=-=-=-=\n");
        var console = new ConsoleOutput(infoService, print);
        var fileCabinet = new FileCabinet(finder, console);

        var docs = fileCabinet.FindByNumber(number);
        fileCabinet.GetInfo(docs);
    }
}