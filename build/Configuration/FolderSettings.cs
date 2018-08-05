using Nuke.Common;
using Nuke.Common.IO;

namespace Build.Configuration
{
    public class FolderSettings 
    {
        private readonly PathConstruction.AbsolutePath _rootDirectory;
        private readonly PathConstruction.AbsolutePath _backendDirectory;
        public  FolderSettings (PathConstruction.AbsolutePath rootDirectory)
        {
            _rootDirectory = rootDirectory;
            _backendDirectory = _rootDirectory / "backend";
        }
        
        public PathConstruction.AbsolutePath CustomerSpreadCalculatorFolder => _backendDirectory / "src" / "CustomerSpreadCalculator";
        public PathConstruction.AbsolutePath CustomerSpreadCalculatorProject => CustomerSpreadCalculatorFolder / "CustomerSpreadCalculator.csproj";

        public PathConstruction.AbsolutePath CustomerSpreadCalculatorTestsProject => _backendDirectory / "test" / "CustomerSpreadCalculator.Tests" / "CustomerSpreadCalculator.Tests.csproj";
        

        public PathConstruction.AbsolutePath FrontEndDirectory => _rootDirectory /  "frontend";
        public PathConstruction.AbsolutePath FrontEndDistFolder => _rootDirectory / "frontend" / "dist";
    }
}
