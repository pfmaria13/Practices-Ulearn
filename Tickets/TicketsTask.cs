using System.Numerics;

namespace Tickets
{
    public static class TicketsTask
    {
        public static BigInteger Solve(int length, int sumOfDigits)
        {
            if (sumOfDigits % 2 != 0)
                return 0;
            var halfTheSum = sumOfDigits / 2;
            var count = new BigInteger[length + 1, halfTheSum + 1];
            for (var i = 0; i <= length; i++)
                count[i, 0] = 1;
            for (var i = 1; i <= length; i++)
                for (var j = 1; j <= halfTheSum; j++)
                    for (var k = 0; j - k >= 0 && k <= 9; k++)
                        count[i, j] += count[i - 1, j - k];
            return count[length, halfTheSum] * count[length, halfTheSum];
        }
    }
}

