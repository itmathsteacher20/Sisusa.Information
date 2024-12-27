namespace Sisusa.Information.Time;

/// <summary>
/// Represents a year and provides various utility methods and operators for year manipulation.
/// </summary>
public sealed class Year(int value, int minYear = 1900, int maxYear = 2999) : IEquatable<Year>
{
    /// <summary>
    /// Gets the value of the year.
    /// </summary>
    public int Value { get; init; } = IsValid(value) ? ConvertToValidYear(value, maxYear) : throw new InvalidYearException();

    /// <summary>
    /// Smallest year value. 
    /// </summary>
    public int MinYear { get; } = minYear; 

    /// <summary>
    /// Largest year value.
    /// </summary>
    public int MaxYear { get; } = maxYear;
     
    public static Year ThisYear => new(timeProvider.GetCurrentTime().Year);

    /// <summary>
    /// Represents the minimum valid year.
    /// </summary>
    public static readonly Year MinValue = new(1900);

    private static ITimeProvider timeProvider = new SystemTimeProvider();


    public static void UseTimeProvider(ITimeProvider provider)
    {
        timeProvider = provider;
    }

    /// <summary>
    /// Checks if the given year value is valid.
    /// </summary>
    /// <param name="testValue">The year value to test.</param>
    /// <returns>True if the year value is valid, otherwise false.</returns>
    private static bool IsValid(int testValue)
    {
        try
        {
            _ = new DateTime(testValue, 1, 1);
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    /// <summary>
    /// Converts a two-digit year to a full year.
    /// </summary>
    /// <param name="theValue">The two-digit year value.</param>
    /// <returns>The full year value.</returns>
    private static int TurnTwoDigitYearToFullYear(int theValue)
    {
        return theValue switch
        {
            >= 51 and <= 99 => 1900 + theValue,
            >= 0 and <= 50 => 2000 + theValue,
            _ => int.MinValue
        };
    }

    /// <summary>
    /// Converts the given year value to a valid year.
    /// </summary>
    /// <param name="theValue">The year value to convert.</param>
    /// <param name="maxValue">The maximum accepted value for the year.</param>
    /// <returns>The valid year value.</returns>
    private static int ConvertToValidYear(int theValue, int maxValue)
    {
        if (theValue >= MinValue &&
        theValue < maxValue)
        {
            return theValue;
        }

        if (theValue is < 0 or > 99) return MinValue;
        var twoDigit = TurnTwoDigitYearToFullYear(theValue);
        if (twoDigit == int.MinValue)
        {
            throw new InvalidYearException();
        }
        return MinValue;
    }

    /// <summary>
    /// Returns the hash code for this year.
    /// </summary>
    /// <returns>The hash code for this year.</returns>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    /// <summary>
    /// Returns a string representation of this year.
    /// </summary>
    /// <returns>A string representation of this year.</returns>
    public override string ToString()
    {
        return Value.ToString();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current year.
    /// </summary>
    /// <param name="obj">The object to compare with the current year.</param>
    /// <returns>True if the specified object is equal to the current year, otherwise false.</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj) || obj is null) return false;

        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            int yr => yr == Value,
            Year testYr => testYr.Value == Value,
            _ => false
        };
    }

    /// <summary>
    /// Determines whether the specified year is equal to the current year.
    /// </summary>
    /// <param name="year">The year to compare with the current year.</param>
    /// <returns>True if the specified year is equal to the current year, otherwise false.</returns>
    public bool Equals(Year? year)
    {
        if (ReferenceEquals(this, year)) return true;
        if (year is null) return false;

        return year.Value == Value;
    }

    /// <summary>
    /// Creates a Year instance from a DateTime object.
    /// </summary>
    /// <param name="dateTime">The DateTime object.</param>
    /// <returns>A Year instance representing the year of the DateTime object.</returns>
    public static Year FromDateTime(DateTime dateTime)
    {
        return new(dateTime.Year);
    }

    /// <summary>
    /// Creates a Year instance from an integer value.
    /// </summary>
    /// <param name="yearValue">The integer year value.</param>
    /// <returns>A Year instance representing the given year value.</returns>
    public static Year FromInt(int yearValue) => new(yearValue);

    /// <summary>
    /// Implicitly converts a Year instance to an integer.
    /// </summary>
    /// <param name="theYear">The Year instance.</param>
    public static implicit operator int(Year theYear)
    {
        return theYear?.Value ?? int.MinValue;
    }

    /// <summary>
    /// Determines whether two Year instances are equal.
    /// </summary>
    /// <param name="a">The first Year instance.</param>
    /// <param name="b">The second Year instance.</param>
    /// <returns>True if the two Year instances are equal, otherwise false.</returns>
    public static bool operator ==(Year? a, Year b)
    {
        if (ReferenceEquals(a, b))
            return true;
        if (ReferenceEquals(a, null) || a is null)
            return b is null || ReferenceEquals(b, null);

        return !ReferenceEquals(a, null) && a.Equals(b);
    }

    /// <summary>
    /// Determines whether two Year instances are not equal.
    /// </summary>
    /// <param name="year">The first Year instance.</param>
    /// <param name="other">The second Year instance.</param>
    /// <returns>True if the two Year instances are not equal, otherwise false.</returns>
    public static bool operator !=(Year? year, Year other)
    {
        return !(year == other);
    }

    /// <summary>
    /// Determines whether one Year instance is less than another.
    /// </summary>
    /// <param name="left">The first Year instance.</param>
    /// <param name="right">The second Year instance.</param>
    /// <returns>True if the first Year instance is less than the second, otherwise false.</returns>
    public static bool operator <(Year? left, Year right)
    {
        if (ReferenceEquals(left, null) ||
            ReferenceEquals(right, null)) return false;

        return left.Value < right.Value;
    }

    /// <summary>
    /// Determines whether one Year instance is greater than another.
    /// </summary>
    /// <param name="left">The first Year instance.</param>
    /// <param name="right">The second Year instance.</param>
    /// <returns>True if the first Year instance is greater than the second, otherwise false.</returns>
    public static bool operator >(Year? left, Year right)
    {
        if (ReferenceEquals(left, null) ||
            ReferenceEquals(right, null)) return false;
        return left.Value > right.Value;
    }

    /// <summary>
    /// Adds a number of years to a Year instance.
    /// </summary>
    /// <param name="left">The Year instance.</param>
    /// <param name="years">The number of years to add.</param>
    /// <returns>A new Year instance representing the result.</returns>
    public static Year operator +(Year? left, int years)
    {
        if (left is null) 
            throw new ArgumentNullException(nameof(left));

        return new(left.Value + years);
    }

    /// <summary>
    /// Subtracts a number of years from a Year instance.
    /// </summary>
    /// <param name="left">The Year instance.</param>
    /// <param name="years">The number of years to subtract.</param>
    /// <returns>A new Year instance representing the result.</returns>
    public static Year operator -(Year? left, int years)
    {
        if (left is null) 
            throw new ArgumentNullException(nameof(left));

        return new(left.Value - years);
    }

    /// <summary>
    /// Subtracts one Year instance from another.
    /// </summary>
    /// <param name="left">The first Year instance.</param>
    /// <param name="right">The second Year instance.</param>
    /// <returns>The difference in years between the two Year instances.</returns>
    public static int operator -(Year? left, Year right)
    {
        if (left is null || right is null) return int.MinValue;

        return left.Value - right.Value;
    }
}

/// <summary>
/// Provides extension methods for the Year class.
/// </summary>
public static class YearExtensions
{
    /// <summary>
    /// Determines whether the year is within the specified range.
    /// </summary>
    /// <param name="instance">The Year instance.</param>
    /// <param name="minYear">The minimum year of the range.</param>
    /// <param name="maxYear">The maximum year of the range.</param>
    /// <returns>True if the year is within the range, otherwise false.</returns>
    public static bool IsWithinRange(this Year instance, Year minYear, Year maxYear)
    {
        return instance >= minYear && instance <= maxYear;
    }

    /// <summary>
    /// Determines whether the year is within the specified YearRange.
    /// </summary>
    /// <param name="instance">The Year instance.</param>
    /// <param name="yearRange">The YearRange instance.</param>
    /// <returns>True if the year is within the YearRange, otherwise false.</returns>
    public static bool IsWithinRange(this Year instance, YearRange yearRange)
    {
        if (yearRange is null) return false;

        return yearRange.Includes(instance);
    }

    /// <summary>
    /// Determines whether the year is a leap year.
    /// </summary>
    /// <param name="instance">The Year instance.</param>
    /// <returns>True if the year is a leap year, otherwise false.</returns>
    public static bool IsLeapYear(this Year instance)
    {
        return DateTime.IsLeapYear(instance);
    }

    /// <summary>
    /// Determines whether the year is in the past relative to the specified year.
    /// </summary>
    /// <param name="instance">The Year instance.</param>
    /// <param name="checkYear">The year to compare against.</param>
    /// <returns>True if the year is in the past, otherwise false.</returns>
    public static bool IsPast(this Year instance, Year checkYear)
    {
        return checkYear > instance;
    }

    /// <summary>
    /// Moves the year forward by the specified number of years.
    /// </summary>
    /// <param name="instance">The Year instance.</param>
    /// <param name="noOfYears">The number of years to move forward.</param>
    /// <returns>A new Year instance representing the result.</returns>
    public static Year ForwardBy(this Year instance, int noOfYears)
    {
        return instance + noOfYears;
    }

    /// <summary>
    /// Moves the year backward by the specified number of years.
    /// </summary>
    /// <param name="instance">The Year instance.</param>
    /// <param name="noOfYears">The number of years to move backward.</param>
    /// <returns>A new Year instance representing the result.</returns>
    public static Year GoBackBy(this Year instance, int noOfYears)
    {
        return instance - noOfYears;
    }
}


/// <summary>
/// Exception thrown when an invalid year is encountered.
/// </summary>
public sealed class InvalidYearException(string msg = "Given digits do not appear to be valid year.") : Exception(msg) { }

/// <summary>
/// Exception thrown when a year is out of the accepted range for the application.
/// </summary>
public sealed class YearOutOfRangeException(string msg = "Year is out of accepted range for application.") : Exception(msg) { }
