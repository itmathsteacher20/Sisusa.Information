namespace Sisusa.Information.Time
{
    public class YearRange : IEquatable<YearRange>
    {
        private YearRange(Year minYear,  Year maxYear)
        {
            if (minYear > maxYear)
            {
                Start = maxYear;
                End = minYear;
            }
            else
            {
                Start = minYear;
                End = maxYear;
            }
        }

        public Year Start { get; private set;}

        public Year End { get; private set;}

        public bool Equals(YearRange? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(other, null)) return false;
            
            return Start == other.Start && End == other.End;
        }

        /// <summary>
        /// Length(in years) between the two years.
        /// </summary>
        public int Length => End - Start;

        public bool Includes(Year testYear)
        {
            if (testYear is null) return false;

            return testYear >= Start && testYear <= End;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            if (ReferenceEquals (obj, null)) return false;
            if (obj is YearRange yearRange)
            {
                return yearRange.Start == Start && yearRange.End == End;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Start} - {End}";
        }

        public static bool operator ==(YearRange a, YearRange b)
        {
            if (ReferenceEquals(a, b)) return true;

            if (a is null) return false;

            return (a.Equals(b));
        }

        public static bool operator !=(YearRange a, YearRange b)
        {
            return !(a == b);
        }

        public static YearRangeBuilder From(Year startYear)
        {
            return new YearRangeBuilder(startYear);
        }

        public class YearRangeBuilder(Year start)
        {
            private Year _start = start;
            private Year _end = Year.MinValue;

            public YearRange To(Year endYear)
            {
                _end = endYear;
                return new YearRange(_start, _end);
            }

        }
    }
}
