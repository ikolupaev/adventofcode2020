using System;
using System.IO;
using System.Text.RegularExpressions;

namespace day02_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var res = 0;
            var regex = new Regex(@"(\d+)-(\d+) (\w): (\w+)");
            foreach (var item in lines)
            {
                var matches = regex.Match(item);
                var min = int.Parse(matches.Groups[1].Value);
                var max = int.Parse(matches.Groups[2].Value);
                var ch = matches.Groups[3].Value[0];
                var password = matches.Groups[4].Value;

                var count = 0;
                foreach (var p in password)
                {
                    if (ch == p) count++;
                    if (count > max) break;
                }

                if (count >= min && count <= max) res++;
            }

            Console.WriteLine(res);
        }
    }
}
