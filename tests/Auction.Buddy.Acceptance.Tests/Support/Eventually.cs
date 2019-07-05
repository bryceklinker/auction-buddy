using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Auction.Buddy.Acceptance.Tests.Support
{
    public static class Eventually
    {
        private const int DefaultWaitMilliseconds = 5000;
        private const int DefaultDelayMilliseconds = 100;

        public static async Task Do(
            Action action, 
            int waitMilliseconds = DefaultWaitMilliseconds,
            int delayMilliseconds = DefaultDelayMilliseconds)
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

        public static async Task DoAsync(
            Func<Task> func,
            int waitMilliseconds = DefaultWaitMilliseconds,
            int delayMilliseconds = DefaultDelayMilliseconds)
        {
            var endWaitingTime = DateTime.UtcNow.AddMilliseconds(waitMilliseconds);
            Exception lastException = null;

            while (endWaitingTime >= DateTime.UtcNow)
            {
                try
                {
                    await func();
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