using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day10
{
    class Program
    {
        static int target;
        static HashSet<int> usedAdapters;
        static int[] volts;
        static Dictionary<int, long> combinationsCache = new Dictionary<int, long>();
        static void Main(string[] args)
        {
            volts = File
                .ReadLines("input.txt")
                .Select(int.Parse)
                .ToArray();

            Array.Sort(volts);

            var diffs = new int[4];
            var last = 0;

            foreach (var item in volts)
            {
                diffs[item - last]++;
                last = item;
            }

            target = volts.Last() + 3;
            usedAdapters = new HashSet<int>();

            var answer2 = CountCombinations(0, 0);

            Console.WriteLine("Answer #2: " + answer2);
        }

        private static long CountCombinations(int volt, int startIndex)
        {
            if(combinationsCache.TryGetValue(volt, out var combinaitons))
            {
                return combinaitons;
            }

            if (startIndex < volts.Length && volts[startIndex] + 3 == target)
            {
                return 1;
            }

            var combinations = 0L;
            
            for (var i = startIndex; i < volts.Length && volt + 3 >= volts[i]; i++)
            {
                if (usedAdapters.Contains(i)) continue;

                usedAdapters.Add(i);
                combinations += CountCombinations(volts[i], i);
                usedAdapters.Remove(i);
            }

            combinationsCache[volt] = combinations;

            return combinations;
        }
    }
}
