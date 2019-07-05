using System;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class OutputHelper
    {
        public static ITestOutputHelper GetOutput()
        {
            try
            {
#pragma warning disable 618
                return (ITestOutputHelper) ScenarioContext.Current.GetBindingInstance(typeof(ITestOutputHelper));
#pragma warning restore 618
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}