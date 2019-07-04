using System.Diagnostics;
using System.Xml.Serialization;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Auction.Buddy.Web.Common.Npm
{
    public class NpmScriptRunner
    {
        private readonly SpaOptions _spaOptions;
        private readonly ILogger _logger;

        private Process ScriptProcess { get; set; }
        private string SourcePath => _spaOptions.SourcePath;
        
        public NpmScriptRunner(ILoggerFactory loggerFactory, SpaOptions spaOptions)
        {
            _spaOptions = spaOptions;
            _logger = loggerFactory.CreateLogger<NpmScriptRunner>();
        }

        public void Execute(string script)
        {
            ScriptProcess = RunNpmScript(script);
        }
        
        private Process RunNpmScript(string script)
        {
            var processStartInfo = new ProcessStartInfo("yarn")
            {
                Arguments = script,
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = SourcePath
            };

            var process = Process.Start(processStartInfo);
            process.EnableRaisingEvents = true;
            SetupProcessOutputCapture(process);
            return process;
        }

        private void SetupProcessOutputCapture(Process process)
        {
            process.ErrorDataReceived += OnErrorReceived;
            process.OutputDataReceived += OnOutputReceived;
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
        }

        private void OnOutputReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e?.Data))
                return;
            
            _logger.LogInformation(e.Data);
        }

        private void OnErrorReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data))
                return;

            _logger.LogError(e.Data);
        }
    }
}