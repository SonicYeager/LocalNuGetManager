using System.CommandLine;

namespace LocalNuGetManager.Operations.Options
{
    public class PackagePathArgument : Argument<string>
    {
        public PackagePathArgument() : base("package-path", "The path to the nuget package")
        {
        }
    }
}
