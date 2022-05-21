using FileCabinet.Models;

namespace FileCabinet.CardInfoChain;

public class MagazineInfoService : CardInfoService
{
    public override string GetInfo(Document document)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        if (document is Magazine magazine)
        {
            return $"{magazine.Title} #{magazine.ReleaseNumber} Published {magazine.DatePublished} by {magazine.Publisher}.";
        }

        return base.GetInfo(document);
    }
}