using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day18_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = File.ReadLines("input.txt").Sum(x => Evaluate(x));
            Console.WriteLine("Answer #1: " + result);
        }

        private static long Evaluate(string expression)
        {
            var queue = ToPolishNotation(expression);
            return Evaluate(queue);
        }

        private static long Evaluate(Queue<string> queue)
        {
            var stack = new Stack<long>();
            foreach (var tok in queue)
            {
                switch (tok)
                {
                    case "+":
                        stack.Push(stack.Pop() + stack.Pop());
                        break;
                    case "*":
                        stack.Push(stack.Pop() * stack.Pop());
                        break;
                    default:
                        stack.Push(long.Parse(tok));
                        break;
                }
            }
            return stack.Pop();
        }

        private static Queue<string> ToPolishNotation(string expression)
        {
            var queue = new Queue<string>();
            var stack = new Stack<string>();

            var prios = "(*+";

            foreach (var item in expression)
            {
                switch (item)
                {
                    case ' ':
                        break;
                    case '(':
                        stack.Push("(");
                        break;
                    case ')':
                        while (stack.Peek() != "(")
                        {
                            queue.Enqueue(stack.Pop());
                        }
                        stack.Pop();
                        break;
                    case '+':
                    case '*':
                        while (stack.Any() && prios.IndexOf(item) <= prios.IndexOf(stack.Peek()))
                        {
                            queue.Enqueue(stack.Pop());
                        }
                        stack.Push(item.ToString());
                        break;
                    default:
                        queue.Enqueue(item.ToString());
                        break;

                }
            }

            while (stack.Any())
            {
                queue.Enqueue(stack.Pop());
            }

            return queue;
        }
    }
}
