using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.Tools.GitVersion;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using Nuke.Common.Tools.Npm;
using Nuke.Common.Tooling;
using static Nuke.Common.Tooling.ProcessTasks;

namespace Build
{
    partial class Build
    {
        Target BuildFrontend  => _ => _
            .Executes(() => 
            {
                NpmTasks.NpmRun(s => s
                    .SetCommand("build")
                    .SetWorkingDirectory(FolderSettings.FrontEndDirectory)
                );
            });
        
        Target UploadFrontend  => _ => _
            .DependsOn(BuildFrontend)
            .Executes(() => 
            {
                var s3Arguments = $"s3 cp {FolderSettings.FrontEndDistFolder} s3://good-analytics --recursive --grants read=uri=http://acs.amazonaws.com/groups/global/AllUsers";
                StartProcess("aws", s3Arguments);
            });
    }
}

