namespace Sisusa.Information.Communication;

/// <summary>
/// Represents a short emergency number such as 112, 999 and so on where the standard Telephone Number standards/rules
/// do not properly apply.
/// </summary>
public class EmergencyTelephoneNumber : TelephoneNumberBase, IEquatable<EmergencyTelephoneNumber>
{
    private readonly uint _number;

    public EmergencyTelephoneNumber(uint number)
    {
        if (number is < 112 or > 9999)
        {
            throw new ArgumentOutOfRangeException(nameof(number), "Emergency number does not appear to be valid.");
        }
        _number = number;
    }
    public override string GetInInternationalNumberFormat()
    {
        return _number.ToString();
    }

    public override string GetCallLink()
    {
        return $"tel:{_number}";
    }

    public override long GetLongPhoneNumber()
    {
        return _number;
    }

    public bool Equals(EmergencyTelephoneNumber? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        return other switch
        {
            null => false,
            not null => other._number == _number
        };
    }

    public override string ToString()
    {
        return _number.ToString();
    }

    public override string ToPrettyString()
    {
        return ToString();
    }

    public override int GetHashCode()
    {
        return _number.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as EmergencyTelephoneNumber);
    }
}