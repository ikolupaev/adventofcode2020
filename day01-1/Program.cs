using System;
using System.Collections.Generic;
using System.IO;

namespace day
{
    class Program
    {
        static void Main(string[] args)
        {
            var set = new HashSet<int>();
            var lines = File.ReadAllLines("input.txt");
            foreach( var line in lines)
            {
                var i = int.Parse(line);
                var couple = 2020 - i;
                if( set.Contains(couple) )
                {
                    Console.WriteLine(i * couple);
                    break;
                }
                set.Add(i);
            }
        }
    }
}
