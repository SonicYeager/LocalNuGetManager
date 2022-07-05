using LocalNuGetManager.Operations.Contracts.Operations;

namespace LocalNuGetManager.Operations.Operations
{
    public class CommandLineInterpreter : ICommandLineInterpreter
    {
        public CommandLineInterpreter()
        {
            
        }
        
        public void InterpretArgs(IEnumerable<string> args)
        {
            throw new NotImplementedException();
            
            //read path (seperate into rootPath/AssemblyName)
            //save/load known nugets
            //increase version
            //save on known nugets
            //run cmds 
            //exit
        }
    }
}
