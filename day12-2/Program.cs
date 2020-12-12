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
                        {
                            var rightRotations = v / 90;
                            for (int i = 0; i < rightRotations; i++)
                            {
                                var t = -dx;
                                dx = dy;
                                dy = t;
                            }
                        }
                        break;
                    case 'L':
                        {
                            var rightRotations = 4 - v / 90;
                            for (int i = 0; i < rightRotations; i++)
                            {
                                var t = -dx;
                                dx = dy;
                                dy = t;
                            }
                            break;
                        }
                    case 'F':
                        x += dx * v;
                        y -= dy * v;
                        break;
                    default:
                        throw new Exception("unknown command " + c);
                }
            }

            Console.WriteLine("Answer #1: " + Math.Abs(x + y));
        }
    }
}
