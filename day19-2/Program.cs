using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            var rule42 = rules[42].Compiled.ToHashSet();
            var rule31 = rules[31].Compiled.ToHashSet();
            var rule42Len = rule42.First().Length;
            var rule31Len = rule31.First().Length;

            while (input.MoveNext())
            {
                var rule31Count = 0;
                var s = input.Current;

                while (s.Length >= rule31Len && rule31.Contains(s[^rule31Len..]))
                {
                    s = s[..^rule31Len];
                    rule31Count++;
                }
                if (rule31Count < 1) continue;

                var rule42Count = 0;
                while (s.Length >= rule42Len && rule42.Contains(s[..rule42Len]))
                {
                    s = s[rule42Len..];
                    rule42Count++;
                }
                if (rule42Count < rule31Count+1) continue;

                if(s.Length == 0) answer++;
            }

            Console.WriteLine(answer);
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
                    if (id == rule.Id)
                    {
                        continue;
                    }
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
