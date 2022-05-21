using FileCabinet.Models;

namespace FileCabinet.CardInfoChain;

public class BookInfoService : CardInfoService
{
    public override string GetInfo(Document document)
    {
        if (document is null)
        {
            throw new ArgumentNullException(nameof(document));
        }

        if (document is Book book)
        {
            return $"{book.Title} (ISBN: {book.ISBN}) Published {book.DatePublished} by {book.Publisher} " +
                   $"with {book.NumberOfPages} pages.";
        }

        return base.GetInfo(document);
    }
}