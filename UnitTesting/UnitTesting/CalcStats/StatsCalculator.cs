namespace CalcStats;

public class StatsCalculator : IStats
{
    private int[] _numbers;

    public StatsCalculator(int[] numbers)
    {
        _numbers = numbers;
    }

    public int GetMinimum()
    {
        throw new NotImplementedException();
    }

    public int GetMaximum()
    {
        throw new NotImplementedException();
    }

    public int GetCount()
    {
        throw new NotImplementedException();
    }

    public float GetAverage()
    {
        throw new NotImplementedException();
    }
}