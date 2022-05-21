using FileCabinet.Models;

namespace FileCabinet.CardInfoChain;

public class LocalizedBookInfoService : CardInfoService
{
    public override string GetInfo(Document document)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        if (document is LocalizedBook book)
        {
            return $"{book.Title} (ISBN: {book.ISBN}) Published {book.DatePublished} by {book.Publisher} " +
                   $"and localized by {book.LocalPublisher} in {book.LocalCountry} " +
                   $"with {book.NumberOfPages} pages.";
        }

        return base.GetInfo(document);
    }
}