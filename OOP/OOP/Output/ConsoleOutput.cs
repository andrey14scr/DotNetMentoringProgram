using FileCabinet.CardInfoChain;
using FileCabinet.Models;
using FileCabinet.Printing;

namespace FileCabinet.Output;

public class ConsoleOutput : IOutputService
{
    public ICardInfoService CardInfoService { get; set; }
    public IConsolePrint ConsolePrint { get; set; }

    public ConsoleOutput(ICardInfoService cardInfoService, IConsolePrint consolePrint)
    {
        CardInfoService = cardInfoService;
        ConsolePrint = consolePrint;
    }

    public void Out(IEnumerable<Document> documents)
    {
        var cards = new List<string>();

        foreach (var document in documents)
        {
            cards.Add(CardInfoService.GetInfo(document));
        }

        ConsolePrint.Print(cards);
    }
}