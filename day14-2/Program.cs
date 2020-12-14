using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day14_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var memory = new Dictionary<ulong, ulong>();
            var maskRegex = new Regex(@"mask = (.+)$");
            var memRegex = new Regex(@"mem\[(?<address>\d+)\] = (?<value>\d+)$");

            var mask1 = 0UL;
            var floatingBits = new int[36];
            var floatingBitsCounter = 0;
            foreach (var line in File.ReadLines("input.txt"))
            {
                var mask = maskRegex.Match(line);
                if (mask.Success)
                {
                    mask1 = 0;
                    floatingBitsCounter = 0;
                    var maskStr = mask.Groups[1].Value;

                    for (var i = 0; i < maskStr.Length; i++)
                    {
                        mask1 <<= 1;

                        if (maskStr[i] == 'X')
                        {
                            floatingBits[floatingBitsCounter++] = maskStr.Length - i - 1;
                        }
                        else
                        {
                            mask1 |= (uint)(maskStr[i] - '0');
                        }
                    }
                }
                else
                {
                    var mem = memRegex.Match(line);
                    var address = ulong.Parse(mem.Groups["address"].Value);
                    var value = ulong.Parse(mem.Groups["value"].Value);

                    SetAddress(memory, value, address | mask1, floatingBits.AsSpan(0, floatingBitsCounter));
                }
            }

            var answer = memory.Values.Aggregate((a, b) => a += b);
            Console.WriteLine("Answer #2: " + answer);
        }

        private static void SetAddress(Dictionary<ulong, ulong> memory, ulong value, ulong address, Span<int> floatingBits)
        {
            if (floatingBits.Length == 0)
            {
                memory[address] = value;
                return;
            }

            SetAddress(memory, value, address & ~(1UL << floatingBits[0]), floatingBits[1..]);
            SetAddress(memory, value, address | 1UL << floatingBits[0], floatingBits[1..]);
        }
    }
}
