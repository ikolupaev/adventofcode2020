using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputNumbers = File
                .ReadLines("input.txt")
                .SelectMany(x => x.Split(','), (c, x) => int.Parse(x))
                .ToArray();

            Console.WriteLine("Answer #1: " + Solve(inputNumbers, 2020));
            Console.WriteLine("Answer #2: " + Solve(inputNumbers, 30000000));
        }

        private static int Solve(int[] inputNumbers, int lastTurn)
        {
            var numbers = new Dictionary<int, int>();

            for (var i = 0; i < inputNumbers.Length - 1; i++)
            {
                numbers.Add(inputNumbers[i], i + 1);
            }

            var lastTurnNumber = inputNumbers.Last();
            var nextTurnNumber = 0;

            for (var turn = inputNumbers.Length; turn < lastTurn; turn++)
            {
                nextTurnNumber = 0;

                if (numbers.TryGetValue(lastTurnNumber, out var prevTurn))
                {
                    nextTurnNumber = turn - prevTurn;
                }

                numbers[lastTurnNumber] = turn;
                lastTurnNumber = nextTurnNumber;
            }

            return nextTurnNumber;
        }
    }
}
