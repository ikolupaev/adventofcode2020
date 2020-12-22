using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace day20
{
    [DebuggerDisplay("{Id}")]
    class Tile
    {
        public int Id;
        public char[][] Raw;

        public int GetTopConnector() => CalcConnector(Raw[0]);
        public int GetRightConnector() => CalcConnector(Raw.Select(x => x[^1]));
        public int GetBottomConnector() => CalcConnector(Raw[^1]);
        public int GetLeftConnector() => CalcConnector(Raw.Select(x => x[0]));

        private int CalcConnector(IEnumerable<char> chars)
        {
            return chars.Aggregate(0, (a, b) => a << 1 | (b == '#' ? 1 : 0));
        }

        internal Tile FlipHorizontal()
        {
            var ret = new char[Raw.Length][];
            for (var i = 0; i < Raw.Length; i++)
            {
                ret[i] = new char[Raw[i].Length];
                for (var j = 0; j < Raw.Length; j++)
                {
                    ret[i][^(j + 1)] = Raw[i][j];
                }
            }

            return new Tile
            {
                Id = Id,
                Raw = ret
            };
        }

        internal Tile Rotate90()
        {
            var ret = new char[Raw.Length][];

            for (var i = 0; i < Raw.Length; i++)
                ret[i] = new char[Raw.Length];

            var n = Raw.Length;
            for (int x = 0; x < Raw.Length / 2; x++)
            {
                for (int y = x; y < Raw.Length - x - 1; y++)
                {
                    ret[x][n - 1 - y] = Raw[y][x];
                    ret[y][x] = Raw[n - 1 - x][y];
                    ret[n - 1 - x][y] = Raw[n - 1 - y][n - 1 - x];
                    ret[n - 1 - y][n - 1 - x] = Raw[x][n - 1 - y];
                }
            }

            return new Tile
            {
                Id = Id,
                Raw = ret
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var r in Raw)
            {
                sb.AppendLine(new string(r));
            }
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = File
                .ReadLines("input.txt")
                .GetEnumerator();

            var tiles = new List<Tile>();

            var rows = new LinkedList<char[]>();
            var id = 0;
            while (input.MoveNext())
            {
                if (input.Current.StartsWith("Tile "))
                {
                    id = int.Parse(input.Current[5..^1]);
                    rows.Clear();
                    continue;
                }

                if (input.Current.Length == 0)
                {
                    tiles.Add(new Tile { Id = id, Raw = rows.ToArray() });
                    continue;
                }

                rows.AddLast(input.Current.ToCharArray());
            }
            tiles.Add(new Tile { Id = id, Raw = rows.ToArray() });

            var columns = (int)Math.Pow(tiles.Count, 0.5);
            var result = new LinkedList<Tile>();
            Place(columns, result, tiles);

            var r = result.ToArray();
            Console.WriteLine("Answer #1: " + (long)r[0].Id * (long)r[columns - 1].Id * (long)r[^columns].Id * (long)r[^1].Id);

            var tileDimention = r[0].Raw.Length - 2;
            var mergedDimention = columns * tileDimention;

            var merged = new char[mergedDimention][];
            for (int i = 0; i < mergedDimention; i++)
            {
                merged[i] = new char[mergedDimention];
            }

            for (var row = 0; row < mergedDimention; row++)
            {
                for (var col = 0; col < mergedDimention; col++)
                {
                    var cc = col % tileDimention;
                    var rr = row % tileDimention;
                    var tile = row / tileDimention * columns + col / tileDimention;
                    merged[row][col] = r[tile].Raw[rr + 1][cc + 1];
                }
            }

            var mergedTile = new Tile { Raw = merged };

            var monster = new char[3][];
            monster[0] = "                  # ".ToCharArray();
            monster[1] = "#    ##    ##    ###".ToCharArray();
            monster[2] = " #  #  #  #  #  #   ".ToCharArray();

            var totalChars = merged.SelectMany(x => x).Count(x => x == '#');
            var totalMonsterChars = monster.SelectMany(x => x).Count(x => x == '#');

            foreach (var x in GetAllRotations(mergedTile))
            {
                var cnt = CountMonsters(x.Raw, monster);

                if (cnt > 0)
                {
                    Console.WriteLine("Answer #2: " + (totalChars - totalMonsterChars * cnt));
                    break;
                }
            }
        }

        private static int CountMonsters(char[][] grid, char[][] monster)
        {
            var monstersCounter = 0;
            for (var row = 0; row < grid.Length - monster.Length; row++)
            {
                for (var col = 0; col < grid[row].Length - monster[0].Length; col++)
                {
                    monstersCounter += IsMonsterHere(grid, monster, row, col) ?1:0;
                }
            }
            return monstersCounter;
        }

        private static bool IsMonsterHere(char[][] grid, char[][] monster, int row, int col)
        {
            for (var r = 0; r < monster.Length; r++)
            {
                for (var c = 0; c < monster[r].Length; c++)
                {
                    if(monster[r][c] == '#' && grid[r+row][c+col] != '#')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool Place(int columns, LinkedList<Tile> grid, List<Tile> tiles)
        {
            if (tiles.Count == 0) return true;

            var row = grid.Count / columns;
            var col = grid.Count % columns;

            Tile leftNeighbour = null;
            if (col > 0) leftNeighbour = grid.Last.Value;

            Tile topNeighbour = null;
            if (row > 0)
            {
                var prev = grid.Last;
                for (int i = 0; i < columns - 1; i++)
                {
                    prev = prev.Previous;
                }
                topNeighbour = prev.Value;

            }

            var nextTiles = tiles.ToList();

            foreach (var t in tiles)
            {
                nextTiles.Remove(t);

                foreach (var vt in GetAllRotations(t))
                {
                    if ((leftNeighbour == null || leftNeighbour.GetRightConnector() == vt.GetLeftConnector()) &&
                        (topNeighbour == null || topNeighbour.GetBottomConnector() == vt.GetTopConnector()))
                    {
                        grid.AddLast(vt);
                        if (Place(columns, grid, nextTiles)) return true;
                        grid.RemoveLast();
                    }
                }

                nextTiles.Add(t);
            }

            return false;
        }

        private static IEnumerable<Tile> GetAllRotations(Tile tile)
        {
            for (int i = 0; i < 4; i++)
            {
                yield return tile;
                yield return tile.FlipHorizontal();
                tile = tile.Rotate90();
            }
        }
    }
}
