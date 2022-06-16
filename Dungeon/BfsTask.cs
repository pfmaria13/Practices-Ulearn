using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            var coordinates = new Queue<SinglyLinkedList<Point>>();
            var visitedСells = new HashSet<Point>();
            var ways = Walker.PossibleDirections.Select(e => new Point(e));
            visitedСells.Add(start);
            coordinates.Enqueue(new SinglyLinkedList<Point>(start, null));
            while (coordinates.Count != 0)
            {
                var result = coordinates.Dequeue();
                var point = result.Value;
                if (!map.InBounds(point)) continue;
                if (map.Dungeon[point.X, point.Y] != MapCell.Empty)
                    continue;
                if (chests.Contains(point)) yield return result;
                AddPont(coordinates, visitedСells, ways, result, point);
            }
        }

        private static void AddPont(Queue<SinglyLinkedList<Point>> coordinates,
            HashSet<Point> visitedСells, IEnumerable<Point> ways, SinglyLinkedList<Point> result, Point point)
        {
            foreach (var e in ways)
            {
                var nextPoint = new Point { X = point.X + e.X, Y = point.Y + e.Y };
                if (!visitedСells.Contains(nextPoint))
                {
                    coordinates.Enqueue(new SinglyLinkedList<Point>(nextPoint, result));
                    visitedСells.Add(nextPoint);
                }
            }
        }
    }
}