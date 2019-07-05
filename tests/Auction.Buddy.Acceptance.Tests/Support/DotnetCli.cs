using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xunit.Abstractions;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class DotnetCli
    {
        private static ITestOutputHelper Output => OutputHelper.GetOutput();
        
        public static string Build(string name, string sourceCodeDirectory)
        {
            var publishDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), name));
            var startInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"dotnet build --configuration Release --output '{publishDirectory}'\"",
                WorkingDirectory = sourceCodeDirectory,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            
            Output?.WriteLine($"Building {name} in directory {sourceCodeDirectory}...");
            var process = Process.Start(startInfo);
            process.WaitForExit();
            Output?.WriteLine($"Built {name} in directory {sourceCodeDirectory}.");

            var buildOutput = process.StandardOutput.ReadToEnd();
            Output?.WriteLine($"{name} Build Output: {buildOutput}");

            if (process.ExitCode != 0)
                throw new InvalidOperationException($"Failed to build {name}");

            return publishDirectory;
        }

        public static Process Start(string dllPath, int port, string workingDirectory)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"{dllPath}",
                WorkingDirectory = workingDirectory,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Environment =
                {
                    new KeyValuePair<string, string>("ASPNETCORE_ENVIRONMENT", "Acceptance"),
                    new KeyValuePair<string, string>("ASPNETCORE_URLS", $"https://localhost:{port}"),
                }
            };
            return Process.Start(startInfo);
        }
    }
}