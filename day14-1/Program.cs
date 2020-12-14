using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day14_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var memory = new List<ulong>();
            var maskRegex = new Regex(@"mask = (.+)$");
            var memRegex = new Regex(@"mem\[(?<address>\d+)\] = (?<value>\d+)$");

            var mask1 = 0UL;
            var mask0 = 0UL;

            foreach (var line in File.ReadLines("input.txt"))
            {
                var mask = maskRegex.Match(line);
                if (mask.Success)
                {
                    mask1 = 0;
                    mask0 = 0;
                    var maskStr = mask.Groups[1].Value.TrimStart('X').ToArray();

                    foreach (var bit in maskStr)
                    {
                        mask0 <<= 1;
                        mask1 <<= 1;

                        switch (bit)
                        {
                            case 'X':
                                mask0 |= 1;
                                mask1 |= 0;
                                break;
                            case '1':
                                mask0 |= 1;
                                mask1 |= 1;
                                break;
                            case '0':
                                mask0 |= 0;
                                mask1 |= 0;
                                break;
                        }
                    }
                }
                else
                {
                    var mem = memRegex.Match(line);
                    var address = int.Parse(mem.Groups["address"].Value);
                    var value = ulong.Parse(mem.Groups["value"].Value);

                    if (memory.Count < address + 1)
                    {
                        memory.AddRange(Enumerable.Repeat(0ul, address - memory.Count + 1));
                    }
                    
                    value |= mask1;
                    value &= mask0;
                    
                    memory[address] = value;
                }
            }

            var answer = memory.Aggregate((a, b) => a += b);
            Console.WriteLine("Answer #1: " + answer);
        }
    }
}
