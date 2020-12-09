using System;
using System.IO;
using System.Linq;

namespace day09
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = File
                .ReadLines("input.txt")
                .Select(long.Parse)
                .ToArray();

            var preamble = 25;
            var answer1 = 0L;
            for (var i = preamble; i < nums.Length; i++)
            {
                var found = false;
                for (int j = i - preamble; j < i - 1; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        if (nums[j] + nums[k] == nums[i])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found) break;
                }

                if (!found)
                {
                    answer1 = nums[i];
                    break;
                }
            }

            Console.WriteLine("Answer #1: " + answer1);

            var sum = nums[0];
            var start = 0;
            var stop = 0;
            while (sum != answer1)
            {
                if (sum < answer1)
                {
                    stop++;
                    sum += nums[stop];
                }
                if (sum > answer1)
                {
                    sum -= nums[start];
                    start++;
                }
            }

            var min = nums[start];
            var max = nums[start];
            for (var i = start; i <= stop; i++)
            {
                if (min > nums[i]) min = nums[i];
                if (max < nums[i]) max = nums[i];
            }

            Console.WriteLine("Answer #2: " + (min + max));
        }
    }
}
