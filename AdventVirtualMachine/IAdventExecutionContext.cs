namespace AdventVirtualMachine
{
    public interface IAdventExecutionContext
    {
        int AX { get; set; }
        int IP { get; set; }
    }
}
