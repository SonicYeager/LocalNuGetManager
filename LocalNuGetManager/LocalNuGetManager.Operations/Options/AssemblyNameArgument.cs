using System.CommandLine;

namespace LocalNuGetManager.Operations.Options
{
    public class AssemblyNameArgument : Argument<string>
    {
        public AssemblyNameArgument() : base("assembly-name", "The name of the assembly to be installed")
        { }
    }
}
