using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryApp.Tests;

[TestClass]
public class SmokeTests
{
    [TestMethod]
    public void Application_Starts_DisplaysMenu_AndExitsCleanly()
    {
        // looks for project file
        string projectPath = FindLibraryAppProject();

        // sets up environment for the application to run
        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                WorkingDirectory = Path.GetDirectoryName(projectPath)!,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        // actually run the application
        process.StartInfo.ArgumentList.Add("run");
        process.StartInfo.ArgumentList.Add("--no-build");

        // run library app project
        process.StartInfo.ArgumentList.Add("--project");

        // location of the project
        process.StartInfo.ArgumentList.Add(projectPath);

        // this should launch the application successfully and display the menu, then exit when option 4 is selected.
        Assert.IsTrue(
            process.Start(),
            "The application process could not be started.");

        // when menu loads, select option 4 to exit the application
        process.StandardInput.WriteLine("4");

        // close input stream, (use this when we will no longer be giving the application any more input)
        process.StandardInput.Close();

        // capture everything the application printed to the console
        string output = process.StandardOutput.ReadToEnd();

        // capture everything the application printed as an error
        string error = process.StandardError.ReadToEnd();

        // did the application exit within 10 seconds?
        Assert.IsTrue(
            process.WaitForExit(10_000),
            "The application did not exit within the timeout.");

        // did the application exit correctly?
        Assert.AreEqual(
            0,
            process.ExitCode,
            $"The application exited with code {process.ExitCode}. Error: {error}");

        // did the application display the menu options?
        StringAssert.Contains(output, "Please choose an option");
        StringAssert.Contains(output, "1. Checkout book");
        StringAssert.Contains(output, "2. Return Book");
        StringAssert.Contains(output, "3. Reserve a Book");
        StringAssert.Contains(output, "4. Exit");

        // did the application exit successfully?
        StringAssert.Contains(output, "Exiting the application.");
    }

    // goes and looks for the LibraryApp project file
    private static string FindLibraryAppProject()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);

        while (directory is not null)
        {
            string projectPath = Path.Combine(
                directory.FullName,
                "LibraryApp",
                "LibraryApp.csproj");

            // if found, return it
            if (File.Exists(projectPath))
            {
                return projectPath;
            }

            directory = directory.Parent;
        }

        Assert.Fail("Could not locate LibraryApp\\LibraryApp.csproj.");
        return string.Empty;
    }
}