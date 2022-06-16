using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var tasks = Enumerable.Range(0, threadsCount).Select((x) =>
            Task.Run(() => SearchBestMove(rocket, new Random(), iterationsCount / threadsCount)));
            var result = Task.WhenAll(tasks).Result;
            var best = result.OrderByDescending(t => t.Item2).First();
            return rocket.Move(best.Item1, level);
        }
    }
}