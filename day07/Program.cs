using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day07
{
    class Bag
    {
        public string Name;
        public List<Bag> Containers = new List<Bag>();
        public Dictionary<string, int> Bags = new Dictionary<string, int>();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bagsRegex = new Regex(@"(?:(?<container>.+)\sbags\scontain\s)(?:(?<content>(?:(?<count>\d+)\s+)*(?<name>.+?))\sbag(?:s*)(?:,\s|\.))+", RegexOptions.Compiled);
            var items = File.ReadLines("input.txt");

            Dictionary<string, Bag> bags = new Dictionary<string, Bag>();

            foreach (var item in items)
            {
                var match = bagsRegex.Match(item);
                if (!match.Success) throw new Exception("error parsing for " + item);

                if (!bags.TryGetValue(match.Groups["container"].Value, out var container))
                {
                    container = new Bag { Name = match.Groups["container"].Value };
                    bags.Add(container.Name, container);
                }

                if (match.Groups["name"].Value == "no other") continue;

                for (var i = 0; i < match.Groups["name"].Captures.Count; i++)
                {
                    var name = match.Groups["name"].Captures[i].Value;
                    var count = int.Parse(match.Groups["count"].Captures[i].Value);
                    if (!bags.TryGetValue(name, out var bag))
                    {
                        bag = new Bag { Name = name };
                        bags.Add(bag.Name, bag);
                    }

                    bag.Containers.Add(container);
                    container.Bags.Add(name, count);
                }
            }

            var gold = bags["shiny gold"];
            var distincContainers = new HashSet<Bag>();

            void dfs(Bag b)
            {
                foreach (var c in b.Containers)
                {
                    distincContainers.Add(c);
                    dfs(c);
                }
            }

            dfs(gold);
            Console.WriteLine(distincContainers.Count);
        }
    }
}
