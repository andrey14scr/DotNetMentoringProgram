namespace Listener;

public static class StringExtension
{
    public static bool EqualsTo(this string s1, string s2)
    {
        return string.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase);
    }
}