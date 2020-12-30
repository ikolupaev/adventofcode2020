using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day21
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputRegex = new Regex(@"(?:(?<i>\w+)\s)+\(contains (?<a>\w+)(?:,\s(?<a>\w+))*\)");

            var items = File
                .ReadLines("input.txt")
                .Select(x => inputRegex.Match(x))
                .Select(x => (
                    Ingredients: x.Groups["i"].Captures.Select(x => x.Value).ToArray(),
                    Allergens: x.Groups["a"].Captures.Select(x => x.Value).ToArray()
                )).ToArray();

            var allergensCandidates = items
                .SelectMany(x => x.Allergens.Select(a => (Allergen: a, Ingredients: x.Ingredients)))
                .GroupBy(
                    x => x.Allergen,
                    x => x.Ingredients.ToList(),
                    (a, ingridients) =>
                    (Allergen: a, Ingredients: ingridients.Aggregate((ac, i) => ac.Intersect(i).ToList())))
                .ToList();

            var allergens = new List<(string Allergen, string Ingridient)>();

            while (allergensCandidates.Any())
            {
                var a = allergensCandidates.Where(x => x.Ingredients.Count == 1).First();
                allergens.Add((a.Allergen, a.Ingredients.First()));
                allergensCandidates.Remove(a);
                foreach (var x in allergensCandidates)
                {
                    x.Ingredients.RemoveAll(x => x == a.Ingredients.First());
                }
            }

            var allergenIngridients = allergens.Select(x => x.Ingridient);

            var nonAllergenIngridients = items
                .SelectMany(x => x.Ingredients)
                .Distinct()
                .Except(allergenIngridients)
                .ToHashSet();

            var answer1 = items
                .SelectMany(x => x.Ingredients)
                .Where(x => nonAllergenIngridients.Contains(x))
                .Count();

            Console.WriteLine("Answer #1: " + answer1);

            var answer2 = string.Join(",", allergens.OrderBy(x => x.Allergen).Select(x => x.Ingridient));

            Console.WriteLine("Answer #2: " + answer2);


        }
    }
}
