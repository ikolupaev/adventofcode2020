namespace AdventVirtualMachine
{
    public class AdventVM: IAdventExecutionContext
    {
        public int AX { get; set; } = 0;
        public int IP { get; set; } = 0;

        public void Execute(AdventCommand command)
        {
            command.Action(this);
            IP++;
        }

        public void Execute(AdventProgram program)
        {
            IP = 0;
            AX = 0;
            foreach (var cmd in program.Commands)
            {
                Execute(cmd);
            }
        }
    }
}
