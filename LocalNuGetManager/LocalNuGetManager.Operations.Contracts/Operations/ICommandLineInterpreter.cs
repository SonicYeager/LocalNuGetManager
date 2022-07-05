namespace LocalNuGetManager.Operations.Contracts.Operations
{
    public interface ICommandLineInterpreter
    {
        public void InterpretArgs(IEnumerable<string> args);
    }
}
