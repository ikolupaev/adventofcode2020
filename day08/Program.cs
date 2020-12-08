﻿using System;
using System.Collections.Generic;
using System.IO;

namespace day08
{
    public class AdventProgram
    {
        public List<AdventCommand> Commands = new List<AdventCommand>();
        
        public void Compile(IEnumerable<string> source)
        {
            foreach (var command in source)
            {
                Commands.Add(CompileOne(command));
            }
        }

        public AdventCommand CompileOne(string command)
        {
            var segments = command.Split();
            switch (segments[0])
            {
                case "acc":
                    {
                        var arg = int.Parse(segments[1]);
                        return new AdventCommand(cx => cx.AX += arg, command);
                    }
                case "jmp":
                    {
                        var arg = int.Parse(segments[1]);
                        return new AdventCommand(cx => cx.SI += arg - 1, command);
                    }
                case "nop":
                    {
                        return new AdventCommand(cx => { }, command);
                    }
                default:
                    throw new ArgumentException("unknown command:" + command);
            }
        }
    }

    public class AdventCommand
    {
        public Action<IAdventExecutionContext> Action;
        public string Text;

        public AdventCommand(Action<IAdventExecutionContext> action, string text)
        {
            Action = action;
            Text = text;
        }
    }

    public interface IAdventExecutionContext
    {
        int AX { get; set; }
        int SI { get; set; }
    }

    public class AdventVm : IAdventExecutionContext
    {
        public int AX { get; set; } = 0;
        public int SI { get; set; } = 0;

        public void Execute(AdventCommand command)
        {
            command.Action(this);
            SI++;
        }

        public void Execute(AdventProgram program)
        {
            SI = 0;
            AX = 0;
            foreach (var cmd in program.Commands)
            {
                Execute(cmd);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var program = new AdventProgram();
            program.Compile(File.ReadLines("input.txt"));

            var vm = new AdventVm();

            var executed = new HashSet<int>();
            while (!executed.Contains(vm.SI))
            {
                executed.Add(vm.SI);
                vm.Execute(program.Commands[vm.SI]);
            }

            Console.WriteLine("Answer #1:" + vm.AX);

            vm.SI = 0;
            executed.Clear();
            var changeIndex = 0;
            while (true)
            {
                executed.Add(vm.SI);
                vm.Execute(program.Commands[vm.SI]);

                if (vm.SI == program.Commands.Count) break;

                if (vm.SI > program.Commands.Count || executed.Contains(vm.SI))
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
                    vm.SI = 0;
                    vm.AX = 0;
                }
            }

            Console.WriteLine("Answer #2:" + vm.AX);
        }
    }
}