using System;
using System.Collections.Generic;

namespace Antiplagiarism
{
    public static class LongestCommonSubsequenceCalculator
    {
        public static List<string> Calculate(List<string> first, List<string> second)
        {
            var opt = CreateOptimizationTable(first, second);
            return RestoreAnswer(opt, first, second);
        }

        private static int[,] CreateOptimizationTable(List<string> first, List<string> second)
        {
            var opt = new int[first.Count + 1, second.Count + 1];
            for (var i = 1; i <= first.Count; i++)
                for (var j = 1; j <= second.Count; j++)
                    if (TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1]) == 0)
                        opt[i, j] = opt[i - 1, j - 1] + 1;
                    else
                        opt[i, j] = Math.Max(opt[i - 1, j], Math.Max(opt[i, j - 1], opt[i - 1, j - 1]));
            return opt;
        }

        private static List<string> RestoreAnswer(int[,] opt, List<string> first, List<string> second)
        {
            var result = new List<string>();
            var lastNumber = opt[first.Count, second.Count];
            if (lastNumber == 0)
                return new List<string>();
            for (var i = first.Count; i >= 1; i--)
                for (var j = second.Count; j >= 1; j--)
                    if (first[i - 1] == second[j - 1] && opt[i, j] == lastNumber)
                    {
                        lastNumber--;
                        result.Add(first[i - 1]);
                        break;
                    }
            result.Reverse();
            return result;
        }
    }
}