using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace day22_2
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

            Play(players, 1);

            var answer = CalcAnswer(players.SelectMany(x => x));

            Console.WriteLine(answer);
        }

        private static long CalcAnswer(IEnumerable<int> cards)
        {
            var count = cards.Count();
            return cards.Select((x, i) => (count - i) * (long)x).Sum();
        }

        private static string CalcPlayersState(List<LinkedList<int>> players)
        {
            return
                string.Join("|", players.Select(x => string.Join(";", x.Select(x => x.ToString()))));
        }

        private static void Play(List<LinkedList<int>> players, int game)
        {
            var states = new HashSet<string>();

            var state = CalcPlayersState(players);

            while (!states.Contains(state) &&
                players.All(x => x.Count > 0))
            {
                states.Add(state);

                var roundCards = players.Select(x => x.First.Value).ToArray();

                foreach (var p in players) p.RemoveFirst();

                var goodForSubGame = true;
                for (var i = 0; i < roundCards.Length; i++)
                {
                    if (players[i].Count < roundCards[i])
                    {
                        goodForSubGame = false;
                        break;
                    }
                }

                var winnerIndex = -1;
                if (goodForSubGame)
                {
                    Debug.Assert(players.All(x => x.Count > 0));

                    var nextDeck = new List<LinkedList<int>>();
                    for (var i = 0; i < roundCards.Length; i++)
                    {
                        nextDeck.Add(new LinkedList<int>(players[i].Take(roundCards[i])));
                    }

                    Play(nextDeck, game + 1);

                    winnerIndex = 0;
                    while (nextDeck[winnerIndex].Count == 0) winnerIndex++;
                }
                else
                {
                    winnerIndex = Array.IndexOf(roundCards, roundCards.Max());
                }

                roundCards = roundCards[winnerIndex..].Concat(roundCards[..winnerIndex]).ToArray();
                foreach (var x in roundCards)
                {
                    players[winnerIndex].AddLast(x);
                }

                state = CalcPlayersState(players);
            }
        }
    }
}
