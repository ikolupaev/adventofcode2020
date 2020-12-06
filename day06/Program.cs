using System;
using System.IO;
using System.Linq;

namespace day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = File.ReadAllLines("input.txt");
            var result = 0;
            var answers = new int[26];
            var groupSize = 0;
            foreach(var x in items)
            {
                if( x.Length == 0)
                {
                    result += answers.Count(x=> x == groupSize);
                    answers = new int[26];
                    groupSize = 0;
                }
                else
                {
                    groupSize++;
                    foreach (var ch in x)
                    {
                        answers[ch-'a']++;
                    }
                }
            }

            result += answers.Count(x => x == groupSize);

            Console.WriteLine(result);
        }
    }
}
