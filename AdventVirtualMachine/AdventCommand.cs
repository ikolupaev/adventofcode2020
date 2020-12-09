using System;

namespace AdventVirtualMachine
{
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
}
