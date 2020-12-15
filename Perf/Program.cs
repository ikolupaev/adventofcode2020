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

            input = File.ReadAllLines("input.txt");
        }

        [Benchmark]
        public void Run14_2_With_IO() => day14_2.Program.Main();
        
        [Benchmark]
        public void Run14_2_No_IO() => day14_2.Program.Solve(input);
    }

    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<test>();
        }
    }
}
