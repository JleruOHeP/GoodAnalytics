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

namespace Build
{
    partial class Build
    {        
        Target CustomerSpreadCalculatorTest => _ => _
                .Executes(() => 
                {
                    //DotNetTest("lambda deploy-function CustomerSpreadCalculator", CustomerSpreadCalculatorFolder);                
                });

        Target DeployCustomerSpreadCalculator => _ => _
                .Executes(() => 
                {
                    DotNet("lambda deploy-function CustomerSpreadCalculator", FolderSettings.CustomerSpreadCalculatorFolder);                
                });
    }
}

