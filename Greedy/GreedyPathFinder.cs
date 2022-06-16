using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            var chests = state.Chests.ToHashSet();
            var energy = 0;
            var goal = 0;
            var pathFinder = new DijkstraPathFinder();
            var ways = new List<Point>();
            var start = state.Position;
            while (goal < state.Goal && energy < state.Energy)
            {
                var pathsToChests = pathFinder.GetPathsByDijkstra(state, start, chests);
                var shortWay = pathsToChests.FirstOrDefault();
                if (shortWay == null)
                    return new List<Point>();
                goal++;
                energy += shortWay.Cost;
                ways.AddRange(shortWay.Path.Skip(1));
                start = shortWay.End;
                chests.Remove(start);
            }
            return energy > state.Energy || goal < state.Goal ? new List<Point>() : ways;
        }
    }
}