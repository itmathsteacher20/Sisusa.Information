namespace Sisusa.Information.Time
{
    /// <summary>
    /// Represents a range of dates with a start and end date.
    /// </summary>
    public class DateRange
    {
        /// <summary>
        /// Gets the start date of the range.
        /// </summary>
        public DateTime Start { get; init; }

        /// <summary>
        /// Gets the end date of the range.
        /// </summary>
        public DateTime End { get; init; }

        /// <summary>
        /// Gets the duration of the date range.
        /// </summary>
        public TimeSpan Duration => End.Subtract(Start);

        /// <summary>
        /// Determines whether the specified date is within the date range.
        /// </summary>
        /// <param name="value">The date to check.</param>
        /// <returns>True if the date is within the range; otherwise, false.</returns>
        public bool Includes(DateTime value)
        {
            return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Start} - {End}";
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj is DateRange dRange)
            {
                return dRange.Start.Equals(Start) && dRange.End.Equals(End);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified DateRange is equal to the current DateRange.
        /// </summary>
        /// <param name="other">The DateRange to compare with the current DateRange.</param>
        /// <returns>True if the specified DateRange is equal to the current DateRange; otherwise, false.</returns>
        public bool Equals(DateRange? other)
        {
            if (other is null) return false;

            return Equals(this, other);
        }

        /// <summary>
        /// Determines whether two DateRange instances are equal.
        /// </summary>
        /// <param name="left">The first DateRange to compare.</param>
        /// <param name="right">The second DateRange to compare.</param>
        /// <returns>True if the two DateRange instances are equal; otherwise, false.</returns>
        public static bool operator ==(DateRange? left, DateRange right)
        {
            if (left is null) return false;
            if (ReferenceEquals(left, right)) return true;

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two DateRange instances are not equal.
        /// </summary>
        /// <param name="left">The first DateRange to compare.</param>
        /// <param name="right">The second DateRange to compare.</param>
        /// <returns>True if the two DateRange instances are not equal; otherwise, false.</returns>
        public static bool operator !=(DateRange? left, DateRange right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Initializes a new instance of the DateRange class with the specified start and end dates.
        /// </summary>
        /// <param name="startValue">The start date of the range.</param>
        /// <param name="endValue">The end date of the range.</param>
        private DateRange(DateTime startValue, DateTime endValue)
        {
            if (startValue.CompareTo(endValue) > 0)
            {
                Start = endValue;
                End = startValue;
            }
            else
            {
                Start = startValue;
                End = endValue;
            }
        }

        /// <summary>
        /// Creates a new DateRangeBuilder with the specified start date.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <returns>A new DateRangeBuilder instance.</returns>
        public static DateRangeBuilder From(DateTime startDate)
        {
            return new DateRangeBuilder(startDate);
        }

        /// <summary>
        /// Provides a builder for creating DateRange instances.
        /// </summary>
        public class DateRangeBuilder
        {
            private DateTime _start;

            /// <summary>
            /// Initializes a new instance of the DateRangeBuilder class with the specified start date.
            /// </summary>
            /// <param name="startDate">The start date of the range.</param>
            public DateRangeBuilder(DateTime startDate)
            {
                _start = startDate;
            }

            /// <summary>
            /// Sets the end date of the range and creates a new DateRange instance.
            /// </summary>
            /// <param name="endDate">The end date of the range.</param>
            /// <returns>A new DateRange instance.</returns>
            public DateRange To(DateTime endDate)
            {
                return new DateRange(_start, endDate);
            }
        }
    }
}
