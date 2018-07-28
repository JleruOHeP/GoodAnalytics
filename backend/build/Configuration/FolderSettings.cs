using Nuke.Common;
using Nuke.Common.IO;

namespace Build.Configuration
{
    public class FolderSettings 
    {
        private readonly PathConstruction.AbsolutePath _rootDirectory;
        public  FolderSettings (PathConstruction.AbsolutePath rootDirectory)
        {
            _rootDirectory = rootDirectory;
        }
        
        public PathConstruction.AbsolutePath CustomerSpreadCalculatorProject => _rootDirectory / "src" / "CustomerSpreadCalculator" / "CustomerSpreadCalculator.csproj";
        public PathConstruction.AbsolutePath CustomerSpreadCalculatorFolder => _rootDirectory / "src" / "CustomerSpreadCalculator";

        public PathConstruction.AbsolutePath FrontEndDirectory => _rootDirectory / ".." / "frontend";
        public PathConstruction.AbsolutePath FrontEndDistFolder => _rootDirectory / ".." / "frontend" / "dist";
    }
}
