using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class OutputHelper
    {
        public static ITestOutputHelper GetOutput()
        {
#pragma warning disable 618
            return (ITestOutputHelper) ScenarioContext.Current.GetBindingInstance(typeof(ITestOutputHelper));
#pragma warning restore 618
        }
    }
}