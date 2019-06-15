using System;
using System.IO;

namespace Auction.Buddy.Mobile.Device.Tests.Support
{
    public static class ApplicationPaths
    {
        public static readonly string SolutionDirectory = ResolvePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", ".."));
        public static readonly string ServerDirectory = ResolvePath(Path.Combine(SolutionDirectory, "src", "Auction.Buddy.Function"));
        public static readonly string AppSettingsPath = ResolvePath(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
        public static readonly string IOSAppPath = ResolvePath(Path.Combine(SolutionDirectory, ""));
        public static readonly string AndroidApkPath = ResolvePath(Path.Combine(SolutionDirectory, ""));

        private static string ResolvePath(string relativePath)
        {
            return Path.GetFullPath(relativePath);
        }
    }
}
