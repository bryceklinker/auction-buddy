using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class OutputHelper
    {
        public static ITestOutputHelper GetOutput()
        {
            return (ITestOutputHelper) ScenarioContext.Current.GetBindingInstance(typeof(ITestOutputHelper));
        }
    }
}