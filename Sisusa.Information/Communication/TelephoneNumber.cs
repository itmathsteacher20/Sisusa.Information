using System.Text.Json.Serialization;

namespace Sisusa.Information.Communication
{
    /// <summary>
    /// Represents a telephone number with country code and phone number.
    /// </summary>
    public class TelephoneNumber : TelephoneNumberBase, IEquatable<TelephoneNumber>
    {
        /// <summary>
        /// Gets the country code of the telephone number.
        /// </summary>
        public uint CountryCode { get; init; }

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        public ulong PhoneNumber { get; init; }
        

        /// <summary>
        /// Represents a null telephone number.
        /// </summary>
        public static readonly TelephoneNumber Null = new(0, 0);

        /// <summary>
        /// For cases where a tel number is not provided.
        /// </summary>
        public static readonly TelephoneNumber None = Null;

        /// <summary>
        /// Returns the telephone number in international format.
        /// </summary>
        /// <returns>The telephone number in international format.</returns>
        public override string GetInInternationalNumberFormat()
        {
            return ToString();
        }

        public override string InInternationalFormat()
        {
            return ToString();
        }

        /// <summary>
        /// Returns a call link for the telephone number.
        /// </summary>
        /// <returns>A call link for the telephone number.</returns>
        public override string GetCallLink()
        {
            return $"tel:{GetInInternationalNumberFormat()}";
        }

        /// <summary>
        /// Validates the format of the telephone number according to ITU E.164 standard.
        /// </summary>
        /// <param name="country">The country code.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>True if the format is valid; otherwise, false.</returns>
        protected static bool IsValidFormat(uint country, ulong phoneNumber)
        {
            if (country == 0 && phoneNumber == 0)
                return true; //

            if (country is < 1 or > 999) return false;
            return phoneNumber.ToString().TrimStart('+').Length <= 12;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            //if (obj is null) return false;
            if (obj is TelephoneNumber tel)
            {
                return tel.CountryCode == CountryCode &&
                       tel.PhoneNumber == PhoneNumber;
            }
            return false;
        }

        /// <inheritdoc/>
        public bool Equals(TelephoneNumber? other)
        {
            if (ReferenceEquals(this, other)) 
                return true;
            
            if (other is null)
                return false;
            
            return other.CountryCode == CountryCode &&
                   other.PhoneNumber == PhoneNumber;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(CountryCode, PhoneNumber);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"+{CountryCode}{PhoneNumber}";
        }

        public override string ToPrettyString()
        {
            var parts = new List<string>();
            var phoneString = PhoneNumber.ToString();
            for (var i = 0; i < phoneString.Length; i+=4)
            {
                int length = Math.Min(4, phoneString.Length - i);
                var part = phoneString.Substring(i, length);
                parts.Add(part);
            }
            return $"+{CountryCode} {string.Join(" ", parts)}";
        }
        
        public override long GetLongPhoneNumber()
        {
            var combined = $"{CountryCode:d3}{PhoneNumber}";
            return long.Parse(combined);
        }

        /// <summary>
        /// Determines whether two <see cref="TelephoneNumber"/> instances are equal.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>True if the instances are equal; otherwise, false.</returns>
        public static bool operator ==(TelephoneNumber? a, TelephoneNumber b)
        {
            if (a is null || ReferenceEquals(a, null))
                return b is null || ReferenceEquals(b, null);

            return ReferenceEquals(a, b) || a.Equals(b);
        }

        /// <summary>
        /// Determines whether two <see cref="TelephoneNumber"/> instances are not equal.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>True if the instances are not equal; otherwise, false.</returns>
        public static bool operator !=(TelephoneNumber? a, TelephoneNumber b)
        {
            return !(a == b);
        }


        /// <summary>
        /// Creates a new telephone number instance from the given params.
        /// Expects phone number to follow the international phone number standard.
        /// </summary>
        /// <param name="countryCode">The country code for the phone number.(Identifies the country in which number is registered)</param>
        /// <param name="phoneNumber">The phone number which combines the area code and subscriber number(where applicable)</param>
        //[JsonConstructor]
        public TelephoneNumber(uint countryCode, ulong phoneNumber)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(phoneNumber, countryCode);
            CountryCode = countryCode;
            if (!IsValidFormat(countryCode, phoneNumber))
            {
                throw new InvalidPhoneNumberPhoneNumberFormatException(nameof(phoneNumber));
            }
            PhoneNumber = phoneNumber;
        }
        
        //[JsonConstructor]
      //  private TelephoneNumber(uint countryCode, ulong phoneNumber):this(countryCode, phoneNumber){}

        /// <summary>
        /// Creates a <see cref="TelNumberBuilder"/> for the specified country code.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>A <see cref="TelNumberBuilder"/> instance.</returns>
        public static TelNumberBuilder ForCountry(uint countryCode)
        {
            return new TelNumberBuilder(countryCode);
        }

        /// <summary>
        /// Builder class for creating <see cref="TelephoneNumber"/> instances.
        /// </summary>
        public class TelNumberBuilder
        {
            private readonly uint _country;

            /// <summary>
            /// Initializes a new instance of the <see cref="TelNumberBuilder"/> class.
            /// </summary>
            /// <param name="countryCode">The country code.</param>
            public TelNumberBuilder(uint countryCode)
            {
                _country = countryCode;
            }
            
            /// <summary>
            /// Creates a telephone number with the specified phone number.
            /// </summary>
            /// <param name="phoneNumber">The phone number.</param>
            /// <returns>A <see cref="TelephoneNumber"/> instance.</returns>
            public TelephoneNumber WithPhoneNumber(ulong phoneNumber)
            {
                return new TelephoneNumber(_country, phoneNumber);
            }
        }
    }

    /// <summary>
    /// Exception thrown when a phone number does not meet ITU E.164 standard.
    /// </summary>
    public sealed class InvalidPhoneNumberPhoneNumberFormatException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPhoneNumberPhoneNumberFormatException"/> class.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        public InvalidPhoneNumberPhoneNumberFormatException(string paramName = "")
            : base("Phone number does not meet ITU E.164 standard for phone numbers.", paramName)
        {
        }
    }
    
     ///<summary>
    /// Exception thrown when a phone number has too few or too many digits.
    /// </summary>
    public sealed class PhoneNumberOutOfRangeException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumberOutOfRangeException"/> class.
        /// </summary>
        /// <param name="paramName">The parameter name.</param>
        public PhoneNumberOutOfRangeException(string paramName = "")
            : base("Phone number has too few or too many digits for specified use!", paramName)
        {
        }
    }
}
