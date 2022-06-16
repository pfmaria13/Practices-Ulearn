using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews
{
    public class ParsingTask
    {
        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            return
               lines.Skip(1)
               .Select(line => line.Split(';'))
               .Select(ParseSlideRecord)
               .Where(slideRecord => slideRecord != null)
               .ToDictionary(slideRecord => slideRecord.SlideId);
        }

        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {
            return
                lines
                .Skip(1)
                .Select(line => ParseVisit(slides, line));
        }

        private static DateTime GetDataTime(string time, string date)
        {
            var stringDate = $"{date} {time}";
            return DateTime.Parse(stringDate);
        }

        private static VisitRecord ParseVisit(IDictionary<int, SlideRecord> slides, string line)
        {
            var part = line.Split(';');
            DateTime d;
            var idUser = 0;
            var idSlide = 0;
            if (part.Length == 4 &&
                int.TryParse(part[0], out idUser) &&
                int.TryParse(part[1], out idSlide) &&
                DateTime.TryParse(part[2], out d) &&
                DateTime.TryParse(part[3], out d) &&
                slides.ContainsKey(idSlide))
                return new VisitRecord(idUser, idSlide,
                                       GetDataTime(part[3], part[2]),
                                       slides[idSlide].SlideType);
            throw new FormatException("Wrong line [" + line + "]");
        }

        private static SlideRecord ParseSlideRecord(string[] data)
        {
            int id;
            if (!int.TryParse(data[0], out id))
                return null;
            if (data.Length != 3)
                return null;
            SlideType type;
            if (!Enum.TryParse(data[1], true, out type))
                return null;
            return new SlideRecord(id, type, data[2]);
        }
    }
}