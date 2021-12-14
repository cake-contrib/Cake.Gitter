#load nuget:?package=Cake.Recipe&version=2.2.1

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Gitter",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Gitter",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunDupFinder: false,
                            shouldRunInspectCode:!AppVeyor.IsRunningOnAppVeyor,
                            shouldRunDotNetCorePack: true,
                            preferredBuildProviderType: BuildProviderType.GitHubActions,
                            shouldGenerateDocumentation: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] {
                            BuildParameters.RootDirectoryPath + "/Source/Cake.Gitter.Tests/*.cs", BuildParameters.RootDirectoryPath + "/Source/Cake.Gitter/**/*.AssemblyInfo.cs", BuildParameters.RootDirectoryPath + "/Source/Cake.Gitter/LitJson/*.cs" },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]*",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

ToolSettings.SetToolPreprocessorDirectives(
    reSharperTools: "#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2021.3.1",
    gitVersionGlobalTool: "#tool dotnet:?package=GitVersion.Tool&version=5.8.1");

Build.RunDotNetCore();
