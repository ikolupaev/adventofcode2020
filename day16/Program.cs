using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day16
{
    public class Field
    {
        public string Name;
        public int[] Ranges;

        public Field(string value, int[] ranges)
        {
            this.Name = value;
            this.Ranges = ranges;
        }

        internal bool IsValid(int value)
        {
            return (value >= Ranges[0] &&
                   value <= Ranges[1]) ||
                   (value >= Ranges[2] &&
                   value <= Ranges[3]);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var fields = new List<Field>();

            var fieldsRegex = new Regex(@"(?<name>.+?):\s(?<r>\d+)-(?<r>\d+) or (?<r>\d+)-(?<r>\d+)", RegexOptions.Compiled);
            var input = File.ReadLines("input.txt").GetEnumerator();

            while (input.MoveNext() && input.Current.Length > 0)
            {
                var f = fieldsRegex.Match(input.Current);

                var field = new Field(
                    f.Groups["name"].Value,
                    f.Groups["r"].Captures.Select(x => int.Parse(x.Value)).ToArray());

                fields.Add(field);
            }

            var validTikets = new List<int[]>();
            var invalidSum = 0;

            while (input.MoveNext())
            {
                if (input.Current.Length == 0 || input.Current.Contains(":")) continue;

                var valid = true;
                var values = input.Current.Split(',').Select(int.Parse).ToArray();
                for (int i = 0; i < values.Length; i++)
                {
                    if (fields.All(x => !x.IsValid(values[i])))
                    {
                        invalidSum += values[i];
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    validTikets.Add(values);
                }
            }

            Console.WriteLine("Answer #1: " + invalidSum);

            var possibleFields = fields.Select(x => fields.ToList()).ToArray();
            var settledFields = new HashSet<Field>();

            while (settledFields.Count < possibleFields.Length)
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    possibleFields[i].RemoveAll(f => validTikets.Any(v => !f.IsValid(v[i])));
                }

                var single = possibleFields
                    .First(x => x.Count == 1 && !settledFields.Contains(x[0]))[0];

                foreach (var possibleField in possibleFields)
                {
                    if (possibleField.Count > 1)
                    {
                        possibleField.RemoveAll(x => x == single);
                    }
                }

                settledFields.Add(single);
            }

            var product = 1L;
            for (int i = 0; i < possibleFields.Length; i++)
            {
                if (possibleFields[i][0].Name.StartsWith("departure"))
                {
                    product *= validTikets[0][i];
                }
            }

            Console.WriteLine("Answer #2: " + product);
        }
    }
}
