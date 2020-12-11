using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day11
{
    struct Vec2
    {
        public int Row;
        public int Col;

        public Vec2(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public static Vec2 operator +(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.Row + v2.Row, v1.Col + v2.Col);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var grid1 = File
                .ReadLines("input.txt")
                .Select(x => x.ToCharArray())
                .ToArray();

            var grid2 = File
                .ReadLines("input.txt")
                .Select(x => x.ToCharArray())
                .ToArray();

            var grid_next = grid2;
            var grid = grid1;

            var gt = new List<Vec2>[grid.Length, grid[0].Length];

            var deltas = new[]
            {
                new Vec2(-1,-1), new Vec2(-1,0), new Vec2(-1,1),
                new Vec2( 0,-1),                 new Vec2( 0,1),
                new Vec2( 1,-1), new Vec2( 1,0), new Vec2( 1,1)
            };

            var height = grid.Length;
            var width = grid[0].Length;

            for (var row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    gt[row, col] = new List<Vec2>();
                    foreach (var dir in deltas)
                    {
                        var p = new Vec2(row + dir.Row, col + dir.Col);
                        while (true)
                        {
                            if (p.Row < 0 || p.Col < 0 || p.Row >= height || p.Col >= width) break;

                            if (grid[p.Row][p.Col] == 'L')
                            {
                                gt[row, col].Add(p);
                                break;
                            }
                            p += dir;
                        }
                    }
                }
            }

            while (true)
            {
                var changed = 0;
                var occuped = 0;
                for (var row = 0; row < height; row++)
                {
                    for (int col = 0; col < width; col++)
                    {
                        grid_next[row][col] = grid[row][col];

                        if (grid[row][col] == '.') continue;

                        var neighbours = gt[row,col].Count(v => grid[v.Row][v.Col] == '#');

                        if (grid[row][col] == 'L' && neighbours == 0)
                        {
                            grid_next[row][col] = '#';
                            changed++;
                        }
                        else if (grid[row][col] == '#' && neighbours > 4)
                        {
                            grid_next[row][col] = 'L';
                            changed++;
                        }

                        if (grid_next[row][col] == '#') occuped++;
                    }
                }

                if (changed == 0)
                {
                    Console.WriteLine("Answer #2: " + occuped);
                    break;
                }

                if (grid == grid1)
                {
                    grid = grid2;
                    grid_next = grid1;
                }
                else
                {
                    grid = grid1;
                    grid_next = grid2;
                }
            }
        }
    }
}
