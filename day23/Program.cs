using System;
using System.Diagnostics;

namespace day23_1
{
    [DebuggerDisplay("{Value} {Picked} > {Next.Value} {Next.Picked}")]
    class Item
    {
        public bool Picked;
        public int Value;
        public Item Next;
        public Item MinusOne;

        public void DumpNext(int count)
        {
            var item = this;
            for (int i = 0; i < count; i++)
            {
                Console.Write(item.Value + " ");
                item = item.Next;
            }
            Console.WriteLine();
        }

        public Item Find(Predicate<Item> func)
        {
            var item = this;
            while (!func(item)) item = item.Next;
            return item;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Solve("716892543", 9, 100);
            Solve("716892543", 1_000_000, 10_000_000);
        }

        static void Solve(string input, int cups, int steps)
        {
            var start = new Item();
            var last = start;

            var min = int.MaxValue;
            var max = int.MinValue;

            foreach (var x in input)
            {
                var v = x - '0';
                min = Math.Min(min, v);
                max = Math.Max(max, v);

                var next = new Item { Value = v };
                last.Next = next;
                next.Next = start.Next;
                last = next;
            }

            for (var i = min + 1; i <= max; i++)
            {
                start.Next.Find(x => x.Value == i).MinusOne = start.Next.Find(x => x.Value == i - 1);
            }

            var minusOne = start.Next.Find(x => x.Value == max);
            while (max < cups)
            {
                max++;
                var next = new Item { Value = max, MinusOne = minusOne };
                last.Next = next;
                next.Next = start.Next;
                minusOne = next;
                last = next;
            }

            start.Next.Find(x => x.Value == min).MinusOne = start.Next.Find(x => x.Value == max);

            var current = start.Next;

            for (int i = 0; i < steps; i++)
            {
                var picked1 = current.Next;
                var picked2 = picked1.Next;
                var picked3 = picked2.Next;

                picked1.Picked = true;
                picked2.Picked = true;
                picked3.Picked = true;

                var destination = current.MinusOne;
                while (destination.Picked) destination = destination.MinusOne;

                picked1.Picked = false;
                picked2.Picked = false;
                picked3.Picked = false;

                current.Next = picked3.Next;
                picked3.Next = destination.Next;
                destination.Next = picked1;

                current = current.Next;
            }

            var one = current.Find(x => x.Value == 1);

            Console.Write("sequence: ");
            one.Next.DumpNext(input.Length - 1);

            Console.WriteLine("products: " + ((long)one.Next.Value * one.Next.Next.Value));
        }
    }
}
