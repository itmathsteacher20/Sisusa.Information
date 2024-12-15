namespace Sisusa.Information.Time;

public sealed class Year(int value): IEquatable<Year>
{
    public int Value {get; init;} = IsValid(value) ? ConvertToValidYear(value) : throw new InvalidYearException();

    private const int MinYearValue = 1900;

    public static readonly Year MinValue = new(MinYearValue);

    private static bool IsValid(int testValue)
    {
        try
        {
            _ = new DateTime(testValue, 1, 1);
            return true;
        }
        catch(ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    private static int TurnTwoDigitYearToFullYear(int theValue)
    {
        return theValue switch
        {
            >= 51 and <= 99 => 1900 + theValue,
            >= 0 and <= 50 => 2000 + theValue,
            _ => int.MinValue
        };
    }

    private static int ConvertToValidYear(int theValue)
    {
        const int maxYear = 2999;
        if (theValue >= MinValue && 
        theValue < maxYear)
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

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public  override bool Equals(object? obj)
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

    public bool Equals(Year? year)
    {
        if (ReferenceEquals(this, year)) return true;
        if (year is null) return false;

        return year.Value == Value;
    }

    public static Year FromDateTime(DateTime dateTime)
    {
        return new(dateTime.Year);
    }

    public static Year FromInt(int yearValue) => new(yearValue);

    public static implicit operator int(Year theYear)
    {
        return theYear?.Value ?? int.MinValue;
    }

    public static bool operator ==(Year a, Year b)
    {
        if (ReferenceEquals (a, b)) return true;
        return !ReferenceEquals(a, null) && a.Equals(b);
    }

    public static bool operator !=(Year year, Year other)
    {
        return !(year == other);
    }
    
    public static bool operator <(Year left, Year right)
    {
        if (ReferenceEquals(left, null) || 
            ReferenceEquals(right, null)) return false;

        return left.Value < right.Value;
    }

    public static bool operator >(Year left, Year right)
    {
        if (ReferenceEquals(left, null) ||
            ReferenceEquals(right, null)) return false;
        //Console.WriteLine($"Left:{left.Value}   Right:{right.Value}  Left>Right? {left.Value > right.Value}");
        return left.Value > right.Value;
    }

    public static Year operator +(Year left, int years)
    {
        if (left is null) throw new ArgumentNullException(nameof(left));

        return new(left.Value + years);
    }

    public static Year operator -(Year left, int years)
    {
        if (left is null) throw new ArgumentNullException(nameof(left));

        return new(left.Value - years);
    }

    public static int operator -(Year left, Year right)
    {
        if (left is null || right is null) return int.MinValue;

        return left.Value - right.Value;
    }
}

public static class YearExtensions
{
    public static bool IsWithinRange(this Year instance, Year minYear, Year maxYear)
    {
        //Console.WriteLine($"{instance}: min-{minYear}, max-{maxYear} \t >={instance >= minYear}, <= {instance <= maxYear}");
        return instance >= minYear && instance <= maxYear;
    }

    public static bool IsWithinRange(this Year instance, YearRange yearRange)
    {
        if (yearRange is null) return false;

        return yearRange.Includes(instance);
    }

    public static bool IsLeapYear(this Year instance)
    {
        return DateTime.IsLeapYear(instance);
    }

    public static bool IsPast(this Year instance, Year checkYear)
    {
        return checkYear > instance;
    }

    public static Year ForwardBy(this Year instance, int noOfYears)
    {
        return instance + noOfYears;
    }

    public static Year GoBackBy(this Year instance, int noOfYears)
    {
        return instance - noOfYears;
    }
}


public sealed class InvalidYearException(string msg="Given digits do not appear to be valid year."):Exception(msg) {}

public sealed class YearOutOfRangeException(string msg="Year is out of accepted range for application."):Exception(msg) {}