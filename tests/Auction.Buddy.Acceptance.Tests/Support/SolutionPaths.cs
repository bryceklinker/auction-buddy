using System.IO;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class SolutionPaths
    {
        public static readonly string SolutionDirectory = Path.GetFullPath(Path.Combine("..", "..", "..", "..", ".."));

        public static readonly string IdentityServerDirectory =
            Path.GetFullPath(Path.Combine(SolutionDirectory, "tests", "Auction.Buddy.Fake.IdentityServer"));

        public static readonly string WebDirectory = Path.GetFullPath(Path.Combine(SolutionDirectory, "src", "Auction.Buddy.Web"));
    }
}