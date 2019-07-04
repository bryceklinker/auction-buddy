using System.Diagnostics;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class ShellCommand
    {
        public static void Execute(string command)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
            };
            var process = Process.Start(startInfo);
            process.WaitForExit();
        }
    }
}