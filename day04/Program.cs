using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace day04_1
{
    class Program
    {
        static HashSet<string> validEyeColor = new HashSet<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        static Regex hclRegex = new Regex(@"^#[\d,a-f]{6}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);
        static Regex pidRegex = new Regex(@"^\d{9}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        static void Main(string[] args)
        {
            var fieldsCount = 0;
            var goodPasports = 0;

            foreach (var line in File.ReadLines("input.txt"))
            {
                if (line.Length == 0)
                {
                    if (fieldsCount == 7) goodPasports++;
                    fieldsCount = 0;
                    continue;
                }

                foreach (var item in line.Split())
                {
                    var kv = item.Split(":");
                    if (kv[0] == "cid") continue;
                    if (IsValid(kv)) fieldsCount++;
                }
            }

            if (fieldsCount == 7) goodPasports++;
            Console.WriteLine(goodPasports);
        }

        private static bool IsValid(string[] kv)
        {
            switch (kv[0])
            {
                case "byr":
                    {
                        if (kv[1].Length != 4 || !int.TryParse(kv[1], out var year)) return false;
                        return year >= 1920 && year <= 2002;
                    }
                case "iyr":
                    {
                        if (kv[1].Length != 4 || !int.TryParse(kv[1], out var year)) return false;
                        return year >= 2010 && year <= 2020;
                    }
                case "eyr":
                    {
                        if (kv[1].Length != 4 || !int.TryParse(kv[1], out var year)) return false;
                        return year >= 2020 && year <= 2030;
                    }
                case "hgt":
                    {
                        switch (kv[1][^2..])
                        {
                            case "cm":
                                {
                                    if (!int.TryParse(kv[1][..^2], out var height)) return false;
                                    return height >= 150 && height <= 193;
                                }
                            case "in":
                                {
                                    if (!int.TryParse(kv[1][..^2], out var height)) return false;
                                    return height >= 59 && height <= 76;
                                }
                            default:
                                return false;
                        }
                    }
                case "hcl":
                    {
                        return kv[1].Length == 7 && hclRegex.IsMatch(kv[1]);
                    }
                case "ecl":
                    {
                        return validEyeColor.Contains(kv[1]);
                    }
                case "pid":
                    {
                        return kv[1].Length == 9 && pidRegex.IsMatch(kv[1]);
                    }
                default:
                    return false;
            }
        }
    }
}
