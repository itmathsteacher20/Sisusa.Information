namespace Sisusa.Information.Communication
{
    public class TelephoneNumber:IEquatable<TelephoneNumber>
    {
        public int CountryCode { get; init; }

        public long PhoneNumber { get; init;}

        public bool IsEmergency { get; protected set; }

        public static readonly TelephoneNumber Null = new(1, 112, true);

        
        public string GetInInternationalNumberFormat()
        {
            return ToString();
        }

        public string GetCallLink()
        {
            return $"tel:{GetInInternationalNumberFormat()}";
        }

        protected static bool IsValidFormat(int country, long phoneNumber)
        {
            //according to ITU E.164 
            if (country < 1 || country > 999) return false;
            return phoneNumber.ToString().TrimStart('+').Length <= 12;
        }

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

        public bool Equals(TelephoneNumber? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;
            
            return other.CountryCode == CountryCode &&
                   other.PhoneNumber == PhoneNumber;
        }

        public override int GetHashCode() 
        {
            return HashCode.Combine(CountryCode, PhoneNumber);
        }

        public override string ToString()
        {
            return $"+{CountryCode}{PhoneNumber}";
        }

        public static bool operator ==(TelephoneNumber a, TelephoneNumber b) 
        {
            if (ReferenceEquals(a, null) || a is null) return false;

            return ReferenceEquals (a, b) || a.Equals(b);
        }

        public static bool operator !=(TelephoneNumber a, TelephoneNumber b)
        {
            return !(a == b);
        }

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
            } else
            {
                PhoneNumber = phoneNumber;
            }
        }

        public static TelNumberBuilder FromCountry(int countryCode)
        {
            return new(countryCode);
        }

        public class TelNumberBuilder(int countryCode)
        {
            private readonly int _country = countryCode;
            
            public TelephoneNumber AsEmergencyNumber(int number)
            {
                return new(_country, number, true);
            }

            public TelephoneNumber HavingPhoneNumber(long phoneNumber)
            {
                return new(_country, phoneNumber);
            }
        }
    }

    public sealed class InvalidPhoneNumberPhoneNumberFormatException(string paramName="") :
        ArgumentException(paramName: paramName, message: "Phone number does not meet ITU E.164 standard for phone numbers.")
    { }
    
    public sealed class PhoneNumberOutOfRangeException(string paramName="") :
        ArgumentException(paramName: paramName, message:"Phone number has too few or too many digits for specified use!") { }
}
