namespace Sisusa.Information.Time
{
    /// <summary>
    /// Provides an interface for obtaining the current time.
    /// </summary>
    public interface ITimeProvider
    {
        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <returns>The current <see cref="DateTime"/>.</returns>
        DateTime GetCurrentTime();

        /// <summary>
        /// Gets the current UTC time.
        /// </summary>
        /// <returns>The current UTC <see cref="DateTime"/>.</returns>
        DateTime GetCurrentUtcTime();

        /// <summary>
        /// Gets the current timestamp.
        /// </summary>
        /// <returns>The current timestamp as a <see cref="long"/>.</returns>
        long GetCurrentTimestamp();
    }

    /// <summary>
    /// Provides the current system time.
    /// </summary>
    public class SystemTimeProvider : ITimeProvider
    {
        /// <summary>
        /// Gets the current time.
        /// </summary>
        /// <returns>The current <see cref="DateTime"/>.</returns>
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// Gets the current UTC time.
        /// </summary>
        /// <returns>The current UTC <see cref="DateTime"/>.</returns>
        public DateTime GetCurrentUtcTime()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the current timestamp.
        /// </summary>
        /// <returns>The current timestamp as a <see cref="long"/>.</returns>
        public long GetCurrentTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}
