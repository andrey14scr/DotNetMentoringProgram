using FileCabinet.Models;

namespace FileCabinet.CardInfoChain;

public class PatentInfoService : CardInfoService
{
    public override string GetInfo(Document document)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        if (document is Patent patent)
        {
            return $"{patent.Title} #{patent.UniqueId} Published {patent.DatePublished} by {patent.Authors} " +
                   $"until {patent.ExpirationDate}.";
        }

        return base.GetInfo(document);
    }
}