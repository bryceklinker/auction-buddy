using System;
using System.Threading.Tasks;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class Eventually
    {
        public static async Task Do(
            Action action, 
            int waitMilliseconds = Timeouts.DefaultWaitMilliseconds,
            int delayMilliseconds = Timeouts.DefaultDelayMilliseconds)
        {
            var endWaitingTime = DateTime.UtcNow.AddMilliseconds(waitMilliseconds);
            Exception lastException = null;

            while (endWaitingTime >= DateTime.UtcNow)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception e)
                {
                    lastException = e;
                    await Task.Delay(delayMilliseconds);
                }
            }

            if (lastException != null)
                throw lastException;
        }
    }
}