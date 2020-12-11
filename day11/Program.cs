using System;
using System.IO;
using System.Linq;

namespace day11
{
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

            while (true)
            {
                var changed = 0;
                var occuped = 0;
                for (var row = 0; row < grid.Length; row++)
                {
                    for (int col = 0; col < grid[row].Length; col++)
                    {
                        grid_next[row][col] = grid[row][col];

                        if (grid[row][col] == '.') continue;
                     
                        var neighbours = CountNeighbours(grid, row, col);
                        
                        if (grid[row][col] == 'L' && neighbours == 0)
                        {
                            grid_next[row][col] = '#';
                            changed++;
                        }
                        else if (grid[row][col] == '#' && neighbours > 3)
                        {
                            grid_next[row][col] = 'L';
                            changed++;
                        }

                        if (grid_next[row][col] == '#') occuped++;
                    }
                }

                if (changed == 0)
                {
                    Console.WriteLine("Answer #1: " + occuped);
                    break;
                }

                if(grid == grid1)
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

        private static int CountNeighbours(char[][] grid, int row, int col)
        {
            int OneIfOccuped(char[][] grid, int row, int col) =>
                row >= 0 && col >= 0 && row < grid.Length && col < grid[row].Length && grid[row][col] == '#' ? 1 : 0;

            return
                OneIfOccuped(grid, row - 1, col - 1) +
                OneIfOccuped(grid, row - 1, col) +
                OneIfOccuped(grid, row - 1, col + 1) +
                OneIfOccuped(grid, row, col - 1) +
                OneIfOccuped(grid, row, col + 1) +
                OneIfOccuped(grid, row + 1, col - 1) +
                OneIfOccuped(grid, row + 1, col) +
                OneIfOccuped(grid, row + 1, col + 1);
        }
    }
}
