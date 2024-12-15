namespace Sisusa.Information.Financial;


public class Money(int cents, Currency currency = Currency.ZAR):IEquatable<Money>
{
        public int InCents { get; init; } = cents;

        public Currency Currency { get; init; } = currency;

        private static readonly Money MinValue = new(0, Currency.ZAR);

        public static readonly Money Zero = MinValue;

        public static Money FromParts(int wholePart, int centPart, Currency currency = Currency.ZAR)
        {
            var cents = wholePart * 100 + centPart;
            return new Money(cents, currency);
        }

        public static Money FromParts(MoneyParts parts, Currency currency = Currency.ZAR)
        {
            return FromParts(parts.WholePart, parts.Cents, currency);
        }

        public decimal ToDecimal()
        {
            return decimal.Divide(InCents, 100m); //Math.Round(InCents / 100m, 2);
        }

        public bool IsDeficit()
        {
            return InCents < 0;
        }

        public Money IncrementByPercent(double percent)
        {
            var increment = (int)Math.Round(InCents * percent / 100);
            return new(InCents + increment, Currency);
        }

        public Money ReduceByPercent(double percent)
        {
            var decrement = (int)Math.Round(InCents * percent / 100);
            return new(InCents - decrement, Currency);
        }

        public override string ToString()
        {
            return $"{Currency} {ToDecimal():N2}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Money) return false;
            if (obj is Money money1)
            {
                return money1.Currency == Currency 
                       && money1.InCents == InCents;
            }
            return false;
        }

        public bool Equals(Money? obj)
        {
            if (obj is null) return false;
            return obj.Currency == Currency &&
                   int.Equals(obj.InCents, InCents);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(InCents, Currency);
        }

        public static bool operator ==(Money left, Money right)
        {
            if (ReferenceEquals(left, null) || ReferenceEquals(right,null))
                return false;
            
            return ReferenceEquals(left, right) ||
                   left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            //if (left is null) return false;
            ArgumentNullException.ThrowIfNull(left);
            return !left.Equals(right);
        }

        public static bool operator <(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            ThrowIfDifferentCurrencies(left, right);
            return left.InCents < right.InCents;
        }

        public static bool operator >(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            ThrowIfDifferentCurrencies(left, right);
            return left.InCents > right.InCents;
        }

        public static bool operator >=(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            ThrowIfDifferentCurrencies(left, right);
            return left.InCents >= right.InCents;
        }

        public static bool operator <=(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            ThrowIfDifferentCurrencies(left, right);
            return left.InCents <= right.InCents;

        }

        public static Money operator +(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
            ThrowIfDifferentCurrencies(left, right);
            return new(left.InCents + right.InCents);
        }

        public static Money operator -(Money left, Money right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            
            ThrowIfDifferentCurrencies(left, right);
            return new(left.InCents - right.InCents);
        }

        public static Money operator *(Money money, int multiplier)
        {
            ArgumentNullException.ThrowIfNull(money);
           
            return new(money.InCents * multiplier);
        }

        public static Money operator *(Money money, decimal multiplier)
        {
            ArgumentNullException.ThrowIfNull(money);
            
            var multiplied = money.InCents * multiplier;
            return new((int)Math.Round(multiplied));
        }
        private static void ThrowIfDifferentCurrencies(Money money1, Money money2,
            string message = "Cannot perform requested operation on monies of different currencies. Convert to same currency first.")
        {
            if (money1.Currency != money2.Currency)
            {
                throw new InvalidOperationException(message);
            }
        }
    }

    public enum Currency
    {
        ZAR,
        USD,
        EUR
    }


    public interface IMoneyConverter
    {
        public Money Convert(Money moneyToConvert, Currency targetCurrency);
    }

    public static class MoneyUtils
    {
        public static bool IsValidCents(int cents)
        {
            return cents is >= 0 and < 100;
        }
    }

    public readonly struct MoneyParts(int whole, int cents)
    {
        public int WholePart { get; } = whole;

        public int Cents { get; } = MoneyUtils.IsValidCents(cents) ? cents:
                throw new ArgumentOutOfRangeException(nameof(cents));
    }

