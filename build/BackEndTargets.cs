using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Build.Configuration;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;

namespace Build
{
    partial class Build
    {        
        Target CustomerSpreadCalculatorTest => _ => _
                .Executes(() => 
                {
                    DotNetTest(s => s.SetProjectFile(FolderSettings.CustomerSpreadCalculatorTestsProject));
                });

        Target DeployCustomerSpreadCalculator => _ => _
                .Executes(() => 
                {
                    DotNet("lambda deploy-function CustomerSpreadCalculator", FolderSettings.CustomerSpreadCalculatorFolder);                
                });
    }
}

