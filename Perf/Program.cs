using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.IO;
using System.Linq;

namespace Perf
{
    [MemoryDiagnoser]
    public class test
    {

        int[] input;
        public test()
        {
            input = File
                .ReadLines("input.txt")
                .SelectMany(x => x.Split(','), (c, x) => int.Parse(x))
                .ToArray();
        }

        [Benchmark]
        public void Run15() => day15.Program.Solve(input, 30000000);
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<test>();
        }
    }
}
