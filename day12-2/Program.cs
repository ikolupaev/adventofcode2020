using System;
using System.IO;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var dx = 10;
            var dy = 1;
            var x = 0;
            var y = 0;

            foreach (var c in File.ReadLines("input.txt"))
            {
                var v = int.Parse(c[1..]);
                var command = c[0];

                switch (command)
                {
                    case 'N':
                        dy += v;
                        break;
                    case 'S':
                        dy -= v;
                        break;
                    case 'E':
                        dx += v;
                        break;
                    case 'W':
                        dx -= v;
                        break;
                    case 'R':
                        Rotate90(ref dx, ref dy, v / 90);
                        break;
                    case 'L':
                        Rotate90(ref dx, ref dy, 4 - v / 90);
                        break;
                    case 'F':
                        x += dx * v;
                        y -= dy * v;
                        break;
                    default:
                        throw new Exception("unknown command " + c);
                }
            }

            Console.WriteLine("Answer #2: " + Math.Abs(x + y));
        }

        private static void Rotate90(ref int dx, ref int dy, int rotationsCount)
        {
            for (int i = 0; i < rotationsCount; i++)
            {
                var t = -dx;
                dx = dy;
                dy = t;
            }
        }
    }
}
