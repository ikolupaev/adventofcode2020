using System;
using System.IO;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var direction = 90;
            var x = 0;
            var y = 0;

            foreach (var c in File.ReadLines("input.txt"))
            {
                var v = int.Parse(c[1..]);
                var command = c[0];

                if (command == 'F')
                {
                    switch (direction)
                    {
                        case 0: command = 'N'; break;
                        case 90: command = 'E'; break;
                        case 180: command = 'S'; break;
                        case 270: command = 'W'; break;
                        default: throw new Exception("unknown direction " + direction);
                    }
                }

                switch (command)
                {
                    case 'N':
                        y -= v;
                        break;
                    case 'S':
                        y += v;
                        break;
                    case 'E':
                        x += v;
                        break;
                    case 'W':
                        x -= v;
                        break;
                    case 'R':
                        direction = (direction + v) % 360;
                        break;
                    case 'L':
                        direction -= v;
                        if (direction < 0) direction += 360;
                        direction %= 360;
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("Answer #1: " + Math.Abs(x + y));
        }
    }
}
