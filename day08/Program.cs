using AdventVirtualMachine;
using System;
using System.Collections.Generic;
using System.IO;

namespace day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new AdventProgram();
            program.Compile(File.ReadLines("input.txt"));

            var vm = new AdventVM();

            var executed = new HashSet<int>();
            while (!executed.Contains(vm.IP))
            {
                executed.Add(vm.IP);
                vm.Execute(program.Commands[vm.IP]);
            }

            Console.WriteLine("Answer #1:" + vm.AX);

            vm.AX = 0;
            vm.IP = 0;
            executed.Clear();
            var changeIndex = 0;
            while (true)
            {
                executed.Add(vm.IP);
                vm.Execute(program.Commands[vm.IP]);

                if (vm.IP == program.Commands.Count) break;

                if (vm.IP > program.Commands.Count || executed.Contains(vm.IP))
                {
                    program = new AdventProgram();
                    program.Compile(File.ReadLines("input.txt"));

                    while (true)
                    {
                        var txt = program.Commands[changeIndex].Text;
                        if (txt.StartsWith("nop") &&
                            int.Parse(txt.Split()[1]) != 0)
                        {
                            program.Commands[changeIndex] = program.CompileOne(txt.Replace("nop", "jmp"));
                            changeIndex++;
                            break;
                        }
                        else if (txt.StartsWith("jmp"))
                        {
                            program.Commands[changeIndex] = program.CompileOne(txt.Replace("jmp", "nop"));
                            changeIndex++;
                            break;
                        }
                        else
                        {
                            changeIndex++;
                        }
                    }

                    executed.Clear();
                    vm.IP = 0;
                    vm.AX = 0;
                }
            }

            Console.WriteLine("Answer #2:" + vm.AX);
        }
    }
}
