using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public abstract class WebApplication
    {
        public string Name { get; }
        public string SourceCodeDirectory { get; }
        public bool HasStarted { get; set; }
        public int Port { get; }
        private ITestOutputHelper Output => OutputHelper.GetOutput();
        private Process Process { get; set; }
        private bool HasExited => Process != null && Process.HasExited;

        protected WebApplication(string name, string sourceCodeDirectory, int port)
        {
            Name = name;
            SourceCodeDirectory = sourceCodeDirectory;
            Port = port;
        }

        public async Task StartAsync()
        {
            if (HasStarted)
                return;
            
            var publishDirectory = DotnetCli.Build(Name, SourceCodeDirectory);
            Process = StartWebApplication(publishDirectory);
            await WaitForWebApplicationToBeReady();
        }

        public Task StopAsync()
        {
            ShellCommand.Execute($"pkill -9 -P {Process.Id}");
            ShellCommand.Execute($"kill -9 {Process.Id}");
            return Task.CompletedTask;
        }

        private Process StartWebApplication(string publishDirectory)
        {
            var process = DotnetCli.Start($"{Path.GetFileName(SourceCodeDirectory)}.dll", Port, publishDirectory);
            process.EnableRaisingEvents = true;
            process.ErrorDataReceived += ProcessOnErrorDataReceived;
            process.OutputDataReceived += OnOutputReceived;
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            return process;
        }

        private void OnOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e?.Data))
                return;

            if (e.Data.Contains("Now listening")) 
                HasStarted = true;
            
            Output?.WriteLine($"{Name}: {e.Data}");
        }

        private void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e?.Data))
                return;
            
            Output?.WriteLine($"{Name} Error: {e.Data}");
        }

        private async Task WaitForWebApplicationToBeReady()
        {
            while (!HasExited && !HasStarted)
            {
                Output?.WriteLine($"Waiting for {Name} to start...");
                await Task.Delay(200);
            }
            Output?.WriteLine($"{Name} has started.");
        }
    }
}