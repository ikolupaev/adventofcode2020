using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day14_2
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Answer #2: " + Solve(File.ReadAllLines("input.txt")));
        }

        public static ulong Solve(string[] lines)
        {
            var memory = new Dictionary<ulong, ulong>();

            var mask1 = 0UL;
            var floatingBits = new int[36];
            var floatingBitsCounter = 0;

            foreach(var line in lines)
            {
                if (line[1] == 'a')
                {
                    mask1 = 0;
                    floatingBitsCounter = 0;

                    for (var i = 7; i < 43; i++)
                    {
                        mask1 <<= 1;

                        if (line[i] == 'X')
                        {
                            floatingBits[floatingBitsCounter++] = 42 - i;
                        }
                        else
                        {
                            mask1 |= (uint)(line[i] - '0');
                        }
                    }
                }
                else
                {
                    var address = 0ul;
                    var i = 4;
                    while (true)
                    {
                        var ch = line[i++];
                        if (ch == ']') break;
                        address = address * 10 + ch - '0';
                    }

                    var value = 0ul;
                    i += 3;
                    while (i < line.Length)
                    {
                        value = value * 10 + line[i++] - '0';
                    }

                    SetAddress(memory, value, address | mask1, floatingBits.AsSpan(0, floatingBitsCounter));
                }
            }

            var answer = 0ul;
            foreach (var item in memory.Values) answer += item;
            
            return answer;
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
