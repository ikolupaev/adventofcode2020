using System;
using System.Collections.Generic;

namespace AdventVirtualMachine
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
                        return new AdventCommand(cx => cx.IP += arg - 1, command);
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
}
