namespace Sisusa.Information.Communication
{
    /// <summary>
    /// Represents a telephone number with country code and phone number.
    /// </summary>
    public class TelephoneNumber : IEquatable<TelephoneNumber>
    {
        /// <summary>
        /// Gets the country code of the telephone number.
        /// </summary>
        public int CountryCode { get; init; }

        /// <summary>
        /// Gets the phone number.
        /// </summary>
        public long PhoneNumber { get; init; }

        /// <summary>
        /// Gets a value indicating whether the number is an emergency number.
        /// </summary>
        public bool IsEmergency { get; protected set; }

        /// <summary>
        /// Represents a null telephone number.
        /// </summary>
        public static readonly TelephoneNumber Null = new(1, 112, true);

        /// <summary>
        /// Returns the telephone number in international format.
        /// </summary>
        /// <returns>The telephone number in international format.</returns>
        public string GetInInternationalNumberFormat()
        {
            return ToString();
        }

        /// <summary>
        /// Returns a call link for the telephone number.
        /// </summary>
        /// <returns>A call link for the telephone number.</returns>
        public string GetCallLink()
        {
            return $"tel:{GetInInternationalNumberFormat()}";
        }

        /// <summary>
        /// Validates the format of the telephone number according to ITU E.164 standard.
        /// </summary>
        /// <param name="country">The country code.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>True if the format is valid; otherwise, false.</returns>
        protected static bool IsValidFormat(int country, long phoneNumber)
        {
            if (country < 1 || country > 999) return false;
            return phoneNumber.ToString().TrimStart('+').Length <= 12;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null) return false;
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
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
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

        /// <summary>
        /// Combines the country code and phone number into a single long value.
        /// </summary>
        /// <returns>The combined long value of the country code and phone number.</returns>
        public long GetLongPhoneNumber()
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
        /// Initializes a new instance of the <see cref="TelephoneNumber"/> class.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="isEmergency">Indicates whether the number is an emergency number.</param>
        /// <exception cref="InvalidPhoneNumberPhoneNumberFormatException">Thrown when the phone number format is invalid.</exception>
        protected TelephoneNumber(int countryCode, long phoneNumber, bool isEmergency = false)
        {
            CountryCode = countryCode;
            IsEmergency = isEmergency;
            if (isEmergency)
            {
                PhoneNumber = phoneNumber;
            }

            if (!IsValidFormat(countryCode, phoneNumber) && !IsEmergency)
            {
                throw new InvalidPhoneNumberPhoneNumberFormatException(nameof(phoneNumber));
            }
            else
            {
                PhoneNumber = phoneNumber;
            }
        }

        /// <summary>
        /// Creates a <see cref="TelNumberBuilder"/> for the specified country code.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>A <see cref="TelNumberBuilder"/> instance.</returns>
        public static TelNumberBuilder FromCountry(int countryCode)
        {
            return new(countryCode);
        }

        /// <summary>
        /// Builder class for creating <see cref="TelephoneNumber"/> instances.
        /// </summary>
        public class TelNumberBuilder
        {
            private readonly int _country;

            /// <summary>
            /// Initializes a new instance of the <see cref="TelNumberBuilder"/> class.
            /// </summary>
            /// <param name="countryCode">The country code.</param>
            public TelNumberBuilder(int countryCode)
            {
                _country = countryCode;
            }

            /// <summary>
            /// Creates an emergency telephone number.
            /// </summary>
            /// <param name="number">The emergency number.</param>
            /// <returns>A <see cref="TelephoneNumber"/> instance.</returns>
            public TelephoneNumber AsEmergencyNumber(int number)
            {
                return new(_country, number, true);
            }

            /// <summary>
            /// Creates a telephone number with the specified phone number.
            /// </summary>
            /// <param name="phoneNumber">The phone number.</param>
            /// <returns>A <see cref="TelephoneNumber"/> instance.</returns>
            public TelephoneNumber HavingPhoneNumber(long phoneNumber)
            {
                return new(_country, phoneNumber);
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
