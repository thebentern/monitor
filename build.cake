var target = Argument("target", "Default");
var solution = "./Monitor.sln";
var configuration = Argument("configuration", "Release");

// Define directories.
var buildDir = Directory("./src/Example/bin") + Directory(configuration);

Task("Clean")
    .Does(() =>
{
    //CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solution);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
      // Use MSBuild
      MSBuild(solution, settings =>
        settings.SetConfiguration(configuration));
    }
    else
    {
      // Use XBuild
      XBuild(solution, settings =>
        settings.SetConfiguration(configuration));
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var testPath = "C:/Users/BenMeadors/Source/Monitor/monitor/Monitor.Tests/bin/Release/Monitor.Tests.dll";//"./Monitor.Tests/bin/" + configuration + "/Monitor.Tests.dll";
    Console.WriteLine(testPath);
    NUnit3(testPath, new NUnit3Settings {
        NoResults = true
        });
});

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

RunTarget(target);
