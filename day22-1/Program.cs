using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day22_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File
                .ReadLines("input.txt")
                .GetEnumerator();

            var players = new List<LinkedList<int>>();
            while (input.MoveNext())
            {
                if (input.Current.Length == 0) continue;

                if (input.Current.StartsWith("Player"))
                {
                    players.Add(new LinkedList<int>());
                    continue;
                }

                if (int.TryParse(input.Current, out var num))
                {
                    players.Last().AddLast(num);
                }
            }

            while (players.All(x => x.Count > 0))
            {
                var roundCards = players.Select(x => x.First.Value).ToArray();
                var maxIndex = Array.IndexOf(roundCards, roundCards.Max());

                roundCards = roundCards[maxIndex..].Concat(roundCards[..maxIndex]).ToArray();
                foreach (var x in roundCards)
                {
                    players[maxIndex].AddLast(x);
                }

                foreach (var p in players)
                {
                    p.RemoveFirst();
                }
            }

            var answer = players.SelectMany(x => x).Reverse().Select((x, i) => (i + 1) * (long)x).Sum();

            Console.WriteLine(answer);
        }
    }
}
