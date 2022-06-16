using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask
    {
        public static List<Point> FindShortestPath(List<Tuple<SinglyLinkedList<Point>, SinglyLinkedList<Point>>> paths)
        {
            if (paths.Count == 0) return null;
            var minValue = int.MaxValue;
            var numberWay = 0;
            for (var i = 0; i < paths.Count; i++)
            {
                var (pathToChests, pathToExit) = paths[i];
                var lengthOfWay = pathToChests.Length + pathToExit.Length;
                if (minValue <= lengthOfWay) continue;
                minValue = lengthOfWay;
                numberWay = i;
            }
            var (shortestToChests, shortestToExit) = paths[numberWay];
            var result = shortestToExit.Skip(1)
                .Aggregate(shortestToChests, (current, point) =>
                    new SinglyLinkedList<Point>(point, current));
            return result.ToList();
        }

        public static MoveDirection[] FindShortestPath(Map map)
        {
            var exitWay = BfsTask.FindPaths(map, map.InitialPosition,
                                            new[] { map.Exit }).FirstOrDefault();
            if (exitWay == null)
                return new MoveDirection[0];
            if (map.Chests.Any(e => exitWay.Contains(e)))
                return MakeDirection(exitWay);
            var chestsWay = BfsTask.FindPaths(map, map.InitialPosition,
                                              map.Chests);
            var path = BfsTask.FindPaths(map, map.Exit, map.Chests);
            var paths = chestsWay.Join(path, listToChests => listToChests.Value,
                                     listToExit => listToExit.Value, Tuple.Create);
            var shortestPath = FindShortestPath(paths.ToList());
            return MakeDirection(shortestPath ?? exitWay.ToList());
        }

        public static MoveDirection[] MakeDirection(IEnumerable<Point> pathToExit)
        {
            var list = pathToExit
                .Reverse()
                .ToList();
            return list.Zip(list.Skip(1), (point1, point2) =>
                Walker.ConvertOffsetToDirection(new Size(point2 - (Size)point1))).ToArray();
        }
    }
}

