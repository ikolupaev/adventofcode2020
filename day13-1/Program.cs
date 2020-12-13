using System;
using System.IO;
using System.Linq;

namespace day13_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var depart = int.Parse(input[0]);
            var buses = input[1].Split(',').Where(x => x != "x").Select(int.Parse);

            var bestWaitTime = int.MaxValue;
            var bestBus = 0;
            foreach (var bus in buses)
            {
                var waitTime = bus - depart % bus;
                if(waitTime < bestWaitTime)
                {
                    bestWaitTime = waitTime;
                    bestBus = bus;
                }
            }

            Console.WriteLine(bestBus * bestWaitTime);
        }
    }
}
