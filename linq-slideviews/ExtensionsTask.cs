using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        public static double Median(this IEnumerable<double> items)
        {
            var sortedList = items.ToList();
            if (sortedList.Count == 0)
                throw new InvalidOperationException();
            sortedList.Sort();
            if (sortedList.Count % 2 != 0)
                return sortedList[sortedList.Count / 2];
            return (sortedList[sortedList.Count / 2] + sortedList[(sortedList.Count / 2) - 1]) / 2;
        }

        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            var item = items.GetEnumerator();
            item.MoveNext();
            var past = item.Current;
            while (item.MoveNext())
            {
                yield return Tuple.Create(past, item.Current);
                past = item.Current;
            }
        }
    }
}