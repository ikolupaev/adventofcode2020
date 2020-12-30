using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day24
{
    class Program
    {
        static void Main(string[] args)
        {
            var tiles = new HashSet<(int X, int Y)>();

            var direcitons = new[] {
                (Direciton: "sw", X: -1, Y:  1),
                (Direciton: "se", X:  1, Y:  1),
                (Direciton: "nw", X: -1, Y: -1),
                (Direciton: "ne", X:  1, Y: -1),
                (Direciton: "e",  X:  2, Y:  0),
                (Direciton: "w",  X: -2, Y:  0)};

            foreach (var item in File.ReadLines("input.txt"))
            {
                var x = 0;
                var y = 0;
                var n = item;

                foreach (var d in direcitons)
                {
                    var originLength = n.Length;
                    n = n.Replace(d.Item1, "");

                    x += (originLength - n.Length) / d.Direciton.Length * d.X;
                    y += (originLength - n.Length) / d.Direciton.Length * d.Y;
                }

                if (!tiles.Add((x, y)))
                {
                    tiles.Remove((x, y));
                }
            }

            Console.WriteLine("Answer #1: " + tiles.Count);

            for (var i = 0; i < 100; i++)
            {
                var tiles1 = tiles.ToHashSet();

                foreach (var tile in tiles.Concat(tiles.SelectMany(t => direcitons.Select(d => (X: t.X + d.X, Y: t.Y + d.Y)))))
                {
                    var neighbours = direcitons.Count(d => tiles.Contains((tile.X + d.X, tile.Y + d.Y)));

                    if (tiles.Contains(tile))
                    {
                        if (neighbours == 0 || neighbours > 2) tiles1.Remove(tile);
                    }
                    else
                    {
                        if (neighbours == 2) tiles1.Add(tile);
                    }
                }

                tiles = tiles1;
            }

            Console.WriteLine("Answer #2: " + tiles.Count);
        }
    }
}
