using System;
using System.IO;

namespace day03_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = File.ReadAllLines("input.txt");
            var width = map[0].Length;
            var deltas = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
            var product = 1L;
            foreach (var (dx, dy) in deltas)
            {
                var count = 0;
                for (int x = 0, y = 0; y < map.Length; x += dx, y += dy)
                {
                    if (map[y][x % width] == '#') count++;
                }
                product *= count;
            }

            Console.WriteLine(product);
        }
    }
}
