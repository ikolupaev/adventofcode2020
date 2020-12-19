using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day19_1
{
    class Rule
    {
        public int Id;
        public string Source;
        public string[] Compiled;
    }

    class Program
    {
        static Dictionary<int, Rule> rules = new();

        static void Main(string[] args)
        {
            var input = File
                .ReadLines("input.txt")
                .GetEnumerator();

            while (input.MoveNext() && input.Current.Length > 0)
            {
                var fragments = input.Current.Split(':');
                rules[int.Parse(fragments[0])] = new Rule
                {
                    Id = int.Parse(fragments[0]),
                    Source = fragments[1].Trim()
                };
            }

            foreach (var x in rules.Keys)
            {
                Compile(rules[x]);
            }

            var answer = 0;
            var rule0 = rules[0].Compiled.ToHashSet();
            while (input.MoveNext())
            {
                if (rule0.Contains(input.Current)) answer++;
            }

            Console.WriteLine("Answer #1: " + answer);
        }

        private static void Compile(Rule rule)
        {
            if (rule.Compiled != null) return;

            if (rule.Source[0] == '"')
            {
                rule.Compiled = new[] { rule.Source.Substring(1, rule.Source.Length - 2) };
                return;
            }

            var compiled = new List<string>();

            void CombineValues(string prefix, Span<string[]> fragments)
            {
                if (fragments.Length == 0)
                {
                    compiled.Add(prefix);
                    return;
                }

                foreach (var s in fragments[0])
                {
                    CombineValues(prefix + s, fragments[1..]);
                }
            }

            foreach (var subRules in rule.Source.Split('|'))
            {
                var fragments = new List<string[]>();
                foreach (var r in subRules.Trim().Split())
                {
                    var id = int.Parse(r.Trim());
                    var rr = rules[id];
                    Compile(rr);
                    fragments.Add(rr.Compiled);
                }

                CombineValues("", fragments.ToArray());
            }

            rule.Compiled = compiled.ToArray();
        }
    }
}
