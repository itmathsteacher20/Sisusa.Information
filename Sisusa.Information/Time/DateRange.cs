namespace Sisusa.Information.Time
{
    public class DateRange
    {
        public DateTime Start { get; init; }

        public DateTime End { get; init; }

        public TimeSpan Duration => End.Subtract(Start);

        public bool Includes(DateTime value)
        {
            return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
        }
        

        public override string ToString()
        {
            return $"{Start} - {End}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

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

        public bool Equals(DateRange? other)
        {
            if (other is null) return false;

            return Equals(this, other);
        }

        public static bool operator ==(DateRange left, DateRange right)
        {
            if (left is null) return false;
            if (ReferenceEquals(left, right)) return true;
            
            return left.Equals(right);
        }

        public static bool operator !=(DateRange left, DateRange right) 
        {
            return !(left == right);
        }

        private DateRange(DateTime startValue, DateTime endValue)
        {
            if (startValue.CompareTo(endValue) > 0)
            {
                Start = endValue;
                End = startValue;
            } else
            {
                Start = startValue;
                End = endValue;
            }
        }

        public static DateRangeBuilder From(DateTime startDate)
        {
            return new DateRangeBuilder(startDate);
        }

        public class DateRangeBuilder(DateTime startDate)
        {
            private DateTime _start = startDate;
            //private DateTime _end = DateTime.MinValue;

            public DateRange To(DateTime  endDate)
            {
                return new DateRange(_start, endDate);
            }
        }
    }
}
