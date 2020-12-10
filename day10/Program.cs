using System;
using System.IO;
using System.Linq;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var volts = File
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

            Console.WriteLine("answer #1: " + diffs[1] * (diffs[3]+1));
        }
    }
}
