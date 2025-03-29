using System.Text.Json.Serialization;

namespace Sisusa.Information.Financial;


/// <summary>
/// Represents a monetary value in a specific currency.
/// </summary>
/// <param name="cents">The amount in cents.</param>
/// <param name="currency">The currency of the money.</param>
public class Money(int cents, Currency currency = Currency.ZAR) : IEquatable<Money>
{
    /// <summary>
    /// Gets the amount in cents.
    /// </summary>
    public int InCents { get; init; } = cents;

    /// <summary>
    /// Gets the currency of the money.
    /// </summary>
    public Currency Currency { get; init; } = currency;
    
    [JsonConstructor]
    private Money(int cents, Currency currency, bool _):this(cents, currency) {}
    
    [JsonConstructor]
    private Money(int cents, bool _):this(cents, Currency.ZAR) { }

    private static readonly Money MinValue = new(0, Currency.ZAR);

    /// <summary>
    /// Represents zero monetary value.
    /// </summary>
    public static readonly Money Zero = MinValue;

    /// <summary>
    /// Creates a new instance of <see cref="Money"/> from whole and cent parts.
    /// </summary>
    /// <param name="wholePart">The whole part of the money.</param>
    /// <param name="centPart">The cent part of the money.</param>
    /// <param name="currency">The currency of the money.</param>
    /// <returns>A new instance of <see cref="Money"/>.</returns>
    public static Money FromParts(int wholePart, int centPart, Currency currency = Currency.ZAR)
    {
        var cents = wholePart * 100 + centPart;
        return new Money(cents, currency);
    }

    /// <summary>
    /// Creates a new instance of <see cref="Money"/> from <see cref="MoneyParts"/>.
    /// </summary>
    /// <param name="parts">The parts of the money.</param>
    /// <param name="currency">The currency of the money.</param>
    /// <returns>A new instance of <see cref="Money"/>.</returns>
    public static Money FromParts(MoneyParts parts, Currency currency = Currency.ZAR)
    {
        return FromParts(parts.WholePart, parts.Cents, currency);
    }

    /// <summary>
    /// Converts the monetary value to a decimal.
    /// </summary>
    /// <returns>The monetary value as a decimal.</returns>
    public decimal ToDecimal()
    {
        return decimal.Divide(InCents, 100m);
    }

    /// <summary>
    /// Determines whether the monetary value is a deficit.
    /// </summary>
    /// <returns><c>true</c> if the monetary value is a deficit; otherwise, <c>false</c>.</returns>
    public bool IsDeficit()
    {
        return InCents < 0;
    }

    /// <summary>
    /// Increments the monetary value by a percentage.
    /// </summary>
    /// <param name="percent">The percentage to increment by.</param>
    /// <returns>A new instance of <see cref="Money"/> with the incremented value.</returns>
    public Money IncrementByPercent(double percent)
    {
        var increment = (int)Math.Round(InCents * percent / 100);
        return new(InCents + increment, Currency);
    }

    /// <summary>
    /// Increase the monetary value by the given percentage.
    /// </summary>
    /// <param name="percent">The whole number percentage to increase by. Assumes all values are in X% form (e.g. 15 for 15%) </param>
    /// <returns></returns>
    public Money IncreaseByPercent(double percent)
    {
        var increment = (int)Math.Round(InCents * percent / 100);
        return new Money(InCents + increment, Currency);
    }

    /// <summary>
    /// Reduces the monetary value by a percentage.
    /// </summary>
    /// <param name="percent">The percentage to reduce by.</param>
    /// <returns>A new instance of <see cref="Money"/> with the reduced value.</returns>
    public Money ReduceByPercent(double percent)
    {
        var decrement = (int)Math.Round(InCents * percent / 100);
        return new(InCents - decrement, Currency);
    }

    /// <summary>
    /// Returns a string representation of the monetary value.
    /// </summary>
    /// <returns>A string representation of the monetary value.</returns>
    public override string ToString()
    {
        return $"{Currency} {ToDecimal():N2}";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Money) return false;
        if (obj is Money money1)
        {
            return money1.Currency == Currency && money1.InCents == InCents;
        }
        return false;
    }

    /// <summary>
    /// Determines whether the specified <see cref="Money"/> is equal to the current <see cref="Money"/>.
    /// </summary>
    /// <param name="obj">The <see cref="Money"/> to compare with the current <see cref="Money"/>.</param>
    /// <returns><c>true</c> if the specified <see cref="Money"/> is equal to the current <see cref="Money"/>; otherwise, <c>false</c>.</returns>
    public bool Equals(Money? obj)
    {
        if (obj is null) return false;
        return obj.Currency == Currency && int.Equals(obj.InCents, InCents);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(InCents, Currency);
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="Money"/> are equal.
    /// </summary>
    /// <param name="left">The first <see cref="Money"/> to compare.</param>
    /// <param name="right">The second <see cref="Money"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Money"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Money? left, Money right)
    {
        if (ReferenceEquals(left, null) || left is null)
            return ReferenceEquals(right, null) || right is null;

        return ReferenceEquals(left, right) || left.Equals(right);
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="Money"/> are not equal.
    /// </summary>
    /// <param name="left">The first <see cref="Money"/> to compare.</param>
    /// <param name="right">The second <see cref="Money"/> to compare.</param>
    /// <returns><c>true</c> if the two <see cref="Money"/> instances are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Money? left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        return !left.Equals(right);
    }

    /// <summary>
    /// Determines whether one specified <see cref="Money"/> is less than another specified <see cref="Money"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Money"/> to compare.</param>
    /// <param name="right">The second <see cref="Money"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="Money"/> is less than the second <see cref="Money"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <(Money? left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        ThrowIfDifferentCurrencies(left, right);
        return left.InCents < right.InCents;
    }

    /// <summary>
    /// Determines whether one specified <see cref="Money"/> is greater than another specified <see cref="Money"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Money"/> to compare.</param>
    /// <param name="right">The second <see cref="Money"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="Money"/> is greater than the second <see cref="Money"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >(Money? left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        ThrowIfDifferentCurrencies(left, right);
        return left.InCents > right.InCents;
    }

    /// <summary>
    /// Determines whether one specified <see cref="Money"/> is greater than or equal to another specified <see cref="Money"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Money"/> to compare.</param>
    /// <param name="right">The second <see cref="Money"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="Money"/> is greater than or equal to the second <see cref="Money"/>; otherwise, <c>false</c>.</returns>
    public static bool operator >=(Money? left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        ThrowIfDifferentCurrencies(left, right);
        return left.InCents >= right.InCents;
    }

    /// <summary>
    /// Determines whether one specified <see cref="Money"/> is less than or equal to another specified <see cref="Money"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Money"/> to compare.</param>
    /// <param name="right">The second <see cref="Money"/> to compare.</param>
    /// <returns><c>true</c> if the first <see cref="Money"/> is less than or equal to the second <see cref="Money"/>; otherwise, <c>false</c>.</returns>
    public static bool operator <=(Money? left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);
        ThrowIfDifferentCurrencies(left, right);
        return left.InCents <= right.InCents;
    }

    /// <summary>
    /// Adds two specified instances of <see cref="Money"/>.
    /// </summary>
    /// <param name="left">The first <see cref="Money"/> to add.</param>
    /// <param name="right">The second <see cref="Money"/> to add.</param>
    /// <returns>A new instance of <see cref="Money"/> that is the sum of the two specified instances.</returns>
    public static Money operator +(Money? left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        ThrowIfDifferentCurrencies(left, right);
        return new(left.InCents + right.InCents);
    }

    /// <summary>
    /// Subtracts one specified instance of <see cref="Money"/> from another.
    /// </summary>
    /// <param name="left">The <see cref="Money"/> to subtract from.</param>
    /// <param name="right">The <see cref="Money"/> to subtract.</param>
    /// <returns>A new instance of <see cref="Money"/> that is the result of the subtraction.</returns>
    public static Money operator -(Money? left, Money right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        ThrowIfDifferentCurrencies(left, right);
        return new(left.InCents - right.InCents);
    }

    /// <summary>
    /// Multiplies a specified instance of <see cref="Money"/> by a specified integer.
    /// </summary>
    /// <param name="money">The <see cref="Money"/> to multiply.</param>
    /// <param name="multiplier">The integer multiplier.</param>
    /// <returns>A new instance of <see cref="Money"/> that is the result of the multiplication.</returns>
    public static Money operator *(Money money, int multiplier)
    {
        ArgumentNullException.ThrowIfNull(money);

        return new(money.InCents * multiplier);
    }

    /// <summary>
    /// Multiplies a specified instance of <see cref="Money"/> by a specified decimal.
    /// </summary>
    /// <param name="money">The <see cref="Money"/> to multiply.</param>
    /// <param name="multiplier">The decimal multiplier.</param>
    /// <returns>A new instance of <see cref="Money"/> that is the result of the multiplication.</returns>
    public static Money operator *(Money money, decimal multiplier)
    {
        ArgumentNullException.ThrowIfNull(money);

        var multiplied = money.InCents * multiplier;
        return new((int)Math.Round(multiplied));
    }

    /// <summary>
    /// Throws an exception if the currencies of the two specified instances of <see cref="Money"/> are different.
    /// </summary>
    /// <param name="money1">The first <see cref="Money"/> to compare.</param>
    /// <param name="money2">The second <see cref="Money"/> to compare.</param>
    /// <param name="message">The exception message.</param>
    /// <exception cref="InvalidOperationException">Thrown if the currencies of the two specified instances of <see cref="Money"/> are different.</exception>
    private static void ThrowIfDifferentCurrencies(Money money1, Money money2, string message = "Cannot perform requested operation on monies of different currencies. Convert to same currency first.")
    {
        if (money1.Currency != money2.Currency)
        {
            throw new InvalidOperationException(message);
        }
    }
}

    
/// <summary>
/// Represents the currency of the money.
/// </summary>
public enum Currency
{
    /// <summary>
    /// South African Rand.
    /// </summary>
    ZAR,

    /// <summary>
    /// United States Dollar.
    /// </summary>
    USD,

    /// <summary>
    /// Euro.
    /// </summary>
    EUR
}

/// <summary>
/// Provides utility methods for <see cref="Money"/>.
/// </summary>
public static class MoneyUtils
{
    /// <summary>
    /// Determines whether the specified cents value is valid.
    /// </summary>
    /// <param name="cents">The cents value to validate.</param>
    /// <returns><c>true</c> if the cents value is valid; otherwise, <c>false</c>.</returns>
    public static bool IsValidCents(int cents)
    {
        return cents is >= 0 and < 100;
    }
}

/// <summary>
/// Represents the whole and cent parts of a monetary value.
/// </summary>
/// <param name="whole">The whole part of the money.</param>
/// <param name="cents">The cent part of the money.</param>
/// <exception cref="ArgumentOutOfRangeException">Thrown when the cent part is not a valid value.</exception>
public readonly struct MoneyParts(int whole, int cents)
{
    /// <summary>
    /// Gets the whole part of the money.
    /// </summary>
    public int WholePart { get; } = whole;

    /// <summary>
    /// Gets the cent part of the money.
    /// </summary>
    public int Cents { get; } = MoneyUtils.IsValidCents(cents) ? cents :
            throw new ArgumentOutOfRangeException(nameof(cents));
}

