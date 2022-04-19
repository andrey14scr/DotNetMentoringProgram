namespace CalcStats;

public class StatsCalculator : IStats
{
    private int[] _numbers;

    public StatsCalculator(int[] numbers)
    {
        _numbers = numbers ?? throw new ArgumentNullException();
    }

    public int GetMinimum()
    {
        if (_numbers.Length == 0)
        {
            throw new InvalidOperationException();
        }

        return _numbers.Min();
    }

    public int GetMaximum()
    {
        if (_numbers.Length == 0)
        {
            throw new InvalidOperationException();
        }

        return _numbers.Max();
    }

    public int GetCount()
    {
        return _numbers.Length;
    }

    public float GetAverage()
    {
        if (_numbers.Length == 0)
        {
            throw new InvalidOperationException();
        }

        return (float)_numbers.Average();
    }
}