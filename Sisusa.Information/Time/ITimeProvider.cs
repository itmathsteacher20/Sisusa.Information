namespace Sisusa.Information.Time
{
    /// <summary>
    /// Provides an interface for obtaining the current time.
    /// </summary>
    /// <remarks>
    /// This is a simple interface that can be implemented to provide the current time. 
    /// It can be useful for testing purposes, allowing you to mock the current time in unit tests.
    /// This interface uses the <see cref="DateTime"/> structure to represent time, and also provides a method to get the current timestamp as a long value.
    /// </remarks>
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

        /// <summary>
        /// Gets the current date and time, including the offset from Coordinated Universal Time (UTC).
        /// </summary>
        /// <returns>A <see cref="DateTimeOffset"/> value representing the current local date and time with the corresponding UTC
        /// offset.</returns>
        DateTimeOffset GetCurrentTimeOffset();

        /// <summary>
        /// Gets the current date and time in Coordinated Universal Time (UTC).
        /// </summary>
        /// <returns>A <see cref="DateTimeOffset"/> value that represents the current UTC date and time.</returns>
        DateTimeOffset GetUtcTime();

        /// <summary>
        /// Retrieves the current number of processor ticks elapsed since the system started.
        /// </summary>
        /// <remarks>Processor ticks are a low-level measure of elapsed time and may be used for
        /// performance monitoring or timing operations. The value is typically monotonically increasing but may wrap
        /// around after a long period depending on the system architecture.</remarks>
        /// <returns>A 64-bit integer representing the number of processor ticks since system startup.</returns>
        long GetProcessorTicks();
    }
}
