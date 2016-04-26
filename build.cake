var target = Argument("target", "Default");
var solution = "./Monitor.sln";
var configuration = Argument("configuration", "Debug");

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

/*Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {

    });*/
Task("Generate-Coverage")
    .Does(() =>
    {
        OpenCover(tool => {
          var testPath = "./Monitor.Tests/bin/" + configuration + "/Monitor.Tests.dll";;
          tool.NUnit3(testPath, new NUnit3Settings {
              NoResults = true
              });
          },
          new FilePath("./result.xml"),
          new OpenCoverSettings(){
              ToolPath = "./tools/Cake/OpenCover.Console.exe"            
            }
            .WithFilter("+[Monitor.Agent.Console]*")
            .WithFilter("+[Monitor.Dashboard.Nancy]*")
            .WithFilter("+[Monitor.Handlers.Core]*")
            .WithFilter("+[Monitor.Handlers.Redis]*")
            //Exclude tests project
            .WithFilter("-[Monitor.Tests]*")
        );
    });

Task("Report-Coverage")
    .IsDependentOn("Generate-Coverage")
    .Does(() =>
    {
          ReportGenerator("./result.xml", "./output", new ReportGeneratorSettings(){
              ToolPath = "./tools/Cake/reportgenerator.exe"
          });
    });


Task("Default")
    .IsDependentOn("Report-Coverage");

RunTarget(target);
