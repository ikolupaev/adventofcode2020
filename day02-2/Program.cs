using System;
using System.IO;
using System.Text.RegularExpressions;

namespace day02_2
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
                var p1 = int.Parse(matches.Groups[1].Value)-1;
                var p2 = int.Parse(matches.Groups[2].Value)-1;
                var ch = matches.Groups[3].Value[0];
                var password = matches.Groups[4].Value;

                if ((password[p1] == ch || password[p2] == ch) && password[p1] != password[p2]) res++;
            }

            Console.WriteLine(res);
        }
    }
}
