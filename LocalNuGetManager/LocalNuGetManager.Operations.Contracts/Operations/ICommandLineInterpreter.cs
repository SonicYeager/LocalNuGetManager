namespace LocalNuGetManager.Operations.Contracts.Operations
{
    public interface ICommandLineInterpreter
    {
        public Task<int> InterpretArgs(IEnumerable<string> args);
    }
}
