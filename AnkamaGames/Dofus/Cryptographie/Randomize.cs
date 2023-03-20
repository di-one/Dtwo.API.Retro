using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace D_One.Core.DofusBehavior.Cryptography
{
    public static class Randomize
    {
        private static int seed = Environment.TickCount;
        private static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));
        private static readonly object blocked = new object();

        public static int GetRandom(int min, int max)
        {
            lock (blocked)
            {
                return min <= max ? random.Value.Next(min, max) : random.Value.Next(max, min);
            }
        }

        public static double GetRandomNumber()
        {
            lock (blocked)
            {
                return random.Value.NextDouble();
            }
        }
    }
}
