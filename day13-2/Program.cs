using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace day13_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var buses = new List<(long Id, long Minute)>();

            long minute = 0;
            foreach (var x in input[1].Split(','))
            {
                if (x != "x")
                {
                    buses.Add((long.Parse(x), minute));
                }
                minute++;
            }

            var timer = Stopwatch.StartNew();
            var commonId = buses[0].Id;
            var commonMinute = 100000000000000L / buses[0].Id * buses[0].Id + buses[0].Id;
            foreach (var (busId, busMinute) in buses.Skip(1))
            {
                while ((commonMinute + busMinute) % busId != 0)
                {
                    commonMinute += commonId;
                }

                commonId *= busId;
            }

            Console.WriteLine("Answer 2: " + commonMinute);
            Console.WriteLine("Elapsed: " + timer.Elapsed);
        }
    }
}
