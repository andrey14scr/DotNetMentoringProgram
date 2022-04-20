namespace LeapYear;

public class YearsService : IYearsService
{
    public YearsType GetYearsType(int year)
    {
        if (year < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
            return YearsType.Leap;

        return YearsType.Common;
    }
}