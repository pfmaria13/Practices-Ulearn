using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            if (visits.Count <= 0)
                return 0;
            var group = visits
                .OrderBy(z => z.DateTime)
                .GroupBy(z => z.UserId)
                .SelectMany(z => z.Select(v => new { v.SlideType, v.DateTime }).Bigrams())
                .Where(z => z.Item1.SlideType == slideType)
                .Select(z => (z.Item2.DateTime - z.Item1.DateTime).TotalMinutes)
                .Where(z => z <= 120 && z >= 1);
            if (group.Count() > 0)
                return group.Median();
            return 0;
        }
    }
}