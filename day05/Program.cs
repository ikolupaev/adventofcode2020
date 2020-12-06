using System;
using System.IO;

namespace day05_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var passes = File.ReadAllLines("input.txt");
            var max = 0;
            var seats = new int[1024];
            foreach (var pass in passes)
            {
                var p = pass;
                var id = Convert.ToInt32(
                    p
                    .Replace('F', '0')
                    .Replace('B', '1')
                    .Replace('L', '0')
                    .Replace('R', '1')
                    , 2);
                max = Math.Max(id, max);
                seats[id] = 1;
            }

            Console.WriteLine(max);

            var seat = 12;
            while(true)
            {
                if (seats[seat] == 0)
                {
                    Console.WriteLine(seat);
                    break;
                }
                seat++;
            }
        }
    }
}
