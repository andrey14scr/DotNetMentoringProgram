namespace HarryPotter;

public class BooksService : IBooksService
{
    private const float Price = 8;

    public float GetCost(int[] books)
    {
        if (books is null)
        {
            throw new ArgumentNullException();
        }

        if (books.Length != 5)
        {
            throw new ArgumentOutOfRangeException();
        }

        if (books.Any(x => x < 0))
        {
            throw new ArgumentException();
        }

        var result = 0f;

        if (books.All(x => x == 0))
        {
            return result;
        }

        var min = books.Where(x => x != 0).Min();
        int count;
        float payment;

        while (min > 0)
        {
            count = books.Count(x => x >= min);
            payment = min * count * Price;

            switch (count)
            {
                case 2:
                    payment *= 0.95f;
                    break;
                case 3:
                    payment *= 0.9f;
                    break;
                case 4:
                    payment *= 0.8f;
                    break;
                case 5:
                    payment *= 0.75f;
                    break;
            }

            result += payment;

            books = books.Select(x => x >= min ? x - min : 0).ToArray();

            if (books.All(x => x == 0))
            {
                break;
            }
            min = books.Where(x => x != 0).Min();
        }

        return result;
    }
}