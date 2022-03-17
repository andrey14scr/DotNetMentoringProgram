using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
            {
                throw new ArgumentNullException(nameof(stringValue));
            }

            if (stringValue.Length == 0)
            {
                throw new FormatException("String was empty.");
            }

            var start = stringValue.Length - 1;

            for (var i = stringValue.Length - 1; i >= 0; i--)
            {
                if (stringValue[i] != ' ')
                {
                    break;
                }

                start--;
            }

            if (start < 0)
            {
                throw new FormatException("String was only whitespaces.");
            }

            int next;
            var result = 0;
            var wasSign = false;

            for (var i = start; i >= 0; i--)
            {
                next = start - i;

                if (next > 10)
                {
                    throw new OverflowException("Number was too big.");
                }

                if (wasSign)
                {
                    throw new FormatException("One sign expected.");
                }

                if (next == 9 && ((stringValue[i] != '0' && stringValue[i] != '1' && stringValue[i] != '2') || (result > 147483647 && i == 0) || (result > 147483648 && i >= 1)))
                {
                    throw new OverflowException("Number was too big.");
                }

                switch (stringValue[i])
                {
                    case '1':
                        result += 1 * TenInPow(next);
                        break;
                    case '2':
                        result += 2 * TenInPow(next);
                        break;
                    case '3':
                        result += 3 * TenInPow(next);
                        break;
                    case '4':
                        result += 4 * TenInPow(next);
                        break;
                    case '5':
                        result += 5 * TenInPow(next);
                        break;
                    case '6':
                        result += 6 * TenInPow(next);
                        break;
                    case '7':
                        result += 7 * TenInPow(next);
                        break;
                    case '8':
                        result += 8 * TenInPow(next);
                        break;
                    case '9':
                        result += 9 * TenInPow(next);
                        break;
                    case '0':
                        break;
                    case '-':
                        wasSign = true;
                        result = -result;
                        break;
                    case '+':
                    case ' ':
                        wasSign = true;
                        break;
                    default:
                        throw new FormatException($"String contained not digit character \'{stringValue[i]}\'.");
                }
            }

            return result;
        }

        private static int TenInPow(int pow)
        {
            var powered = 1;

            for (var i = 0; i < pow; i++)
            {
                powered *= 10;
            }

            return powered;
        }
    }
}