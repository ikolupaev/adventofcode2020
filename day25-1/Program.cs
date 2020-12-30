using System;
using System.Linq;
using System.Threading.Tasks;

namespace day25_1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var publicKeys = new[] { 5249543ul, 14082811ul };

            var tasks = publicKeys.Select(x => Task.Run(() => HackLoopSize(x)));

            var loopSizes = await Task.WhenAll(tasks);

            for (int i = 0; i < publicKeys.Length; i++)
            {
                Console.WriteLine(loopSizes[i] + ": " + Process(publicKeys[publicKeys.Length - i - 1], loopSizes[i]));
            }
        }

        private static ulong HackLoopSize(ulong targetPublicKey)
        {
            var value = 1UL;

            ulong loopSize = 0;
            while (value != targetPublicKey)
            {
                loopSize++;
                value *= 7;
                value %= 20201227;
            }
            return loopSize;
        }

        private static ulong Process(ulong subject, ulong loopSize)
        {
            var value = 1UL;

            for (ulong i = 0; i < loopSize; i++)
            {
                value *= subject;
                value %= 20201227;
            }

            return value;
        }
    }
}
