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
            var count = 0;
            for (int x = 0, y = 0; y < map.Length; x += 3, y++)
            {
                if (map[y][x % width] == '#') count++;
            }

            Console.WriteLine(count);
        }
    }
}
