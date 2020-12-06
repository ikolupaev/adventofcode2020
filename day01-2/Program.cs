using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<int, long>();
            var nums = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
            for (var i = 0; i < nums.Length; i++)
            {
                for (var j = i + 1; j < nums.Length; j++)
                {
                    dict.TryAdd(nums[i] + nums[j], nums[i] * nums[j]);
                }
            }

            foreach (var item in nums)
            {
                var couple = 2020 - item;
                if (dict.TryGetValue(couple, out var prod))
                {
                    Console.WriteLine(prod * item);
                    break;
                }
            }
        }
    }
}
