using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.IO;

namespace Perf
{
    [MemoryDiagnoser]
    public class test
    {

        string[] input;
        public test()
        {
            input = File
                .ReadAllLines("input.txt");
        }

        [Benchmark()]
        public void Run3() => day17.Program.Solve(input, 3);
        [Benchmark]
        public void Run4() => day17.Program.Solve(input, 4);
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<test>();
        }
    }
}
