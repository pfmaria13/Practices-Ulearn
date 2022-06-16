using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Greedy.Architecture;
using Greedy.Architecture.Drawing;

namespace Greedy
{
	public class NotGreedyPathFinder : IPathFinder
	{
		private List<Point> bestPath = new List<Point>();
		private int bestCountOfChests;
		private int countOfChests;

		private void FindPath(State state, Point startPoint, List<Point> remainingСhests,
							   List<Point> passedPath, int energy, int countFoundChests)
		{
			if (bestCountOfChests < countFoundChests)
			{
				bestPath = passedPath;
				bestCountOfChests = countFoundChests;
			}
			if (bestCountOfChests == countOfChests || countFoundChests == countOfChests) return;
			var finder = new DijkstraPathFinder();
			var paths = finder.GetPathsByDijkstra(state, startPoint, remainingСhests)
				.Where(e => energy + e.Cost <= state.Energy);
			foreach (var path in paths)
			{
				remainingСhests.Remove(path.End);
				FindPath(state, path.End, remainingСhests, passedPath.Concat(path.Path.Skip(1)).ToList(),
						  path.Cost + energy, countFoundChests + 1);
				remainingСhests.Add(path.End);
			}
		}

		public List<Point> FindPathToCompleteGoal(State state)
		{
			countOfChests = state.Chests.Count;
			FindPath(state, state.Position, state.Chests, new List<Point>(), 0, 0);
			return bestPath;
		}
	}
}