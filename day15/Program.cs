using System;
using System.IO;
using System.Linq;

namespace day15
{
    public class Program
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

        public static int Solve(int[] inputNumbers, int lastTurn)
        {
            var numbers = new int[lastTurn];

            for (var i = 0; i < inputNumbers.Length - 1; i++)
            {
                numbers[inputNumbers[i]] = i + 1;
            }

            var lastTurnNumber = inputNumbers.Last();
            var nextTurnNumber = 0;

            for (var turn = inputNumbers.Length; turn < lastTurn; turn++)
            {
                nextTurnNumber = 0;
                var prevTurn = numbers[lastTurnNumber];
                if (prevTurn > 0)
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
