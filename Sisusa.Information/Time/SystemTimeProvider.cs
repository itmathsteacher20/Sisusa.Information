namespace Sisusa.Information.Time
{
    /// <summary>
    /// Provides the current system time.
    /// </summary>
    public class SystemTimeProvider : ITimeProvider
    {
        
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }


        public DateTime GetCurrentUtcTime()
        {
            return DateTime.UtcNow;
        }


        public long GetCurrentTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public DateTimeOffset GetCurrentTimeOffset()
        {
            return DateTimeOffset.Now;
        }

        public DateTimeOffset GetUtcTime()
        {
            return DateTimeOffset.UtcNow;
        }

        public long GetProcessorTicks()
        {
            return (long)DateTime.UtcNow.Ticks;
        }
    }
}
