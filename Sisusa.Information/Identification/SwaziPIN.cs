namespace Sisusa.Information.Identification
{
    /// <summary>
    /// A class representing the National Identification Number (Id Number) used in Eswatini
    /// </summary>
    public class SwaziPin
    {
        //private readonly string _pin;

        private readonly PinData _pinData;

        /// <summary>
        /// Gender extracted from the PIN
        /// </summary>
        public Gender Gender => GetGenderFromGenderCode(_pinData.Gender);

        /// <summary>
        /// Date of birth extracted from the PIN.
        /// </summary>
        public DateOnly DateOfBirth =>
            DateOnly.FromDateTime(
                new DateTime(_pinData.Year, _pinData.Month, _pinData.Day)
            );


        /// <summary>
        /// The last 3 digits of the PIN - the serial number.
        /// </summary>
        public int SerialNumber => _pinData.Serial;

        /// <summary>
        /// Creates a new instance from the given PIN string
        /// </summary>
        /// <param name="pin"></param>
        protected SwaziPin(string pin)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(pin, nameof(pin));
          
            if (pin.Any(c => !char.IsDigit(c)))
            {
                throw new InvalidPinFormatException(
                    message: $"The specified PIN `{pin}` contains non-numeric values and is not a valid Eswatini National ID.",
                    paramName:nameof(pin)
                    );
            }

            _pinData = ValidatePinAndExtractParts(pin);
        }

        

        protected static PinData Extract(string pin)
        {
            int year = int.Parse(pin[..2]);
            int month = int.Parse(pin.Substring(2, 2));
            int day = int.Parse(pin.Substring(4, 2));
            int gender = int.Parse(pin.Substring(6, 4));
            int serial = int.Parse(pin.Substring(10, 3));

            if (year <= 30)
            {
                year = 2000 + year;
            } else
            {
                year = 1900 + year;
            }
            return new PinData(year, month, day, gender, serial);
        }

        private static Gender GetGenderFromGenderCode(int genderCode)
        {
            return genderCode switch
            {
                >= 1100 and <= 6100 => Gender.Female,
                >= 6100 and <= 9999 => Gender.Male,
                _ => Gender.Unknown,
            };
        }

        #region Validate_Parts

        private static bool IsValidDate(int year, int month, int day)
        {
            try
            {
                _ = new DateTime(year, month, day);
                return true;
            }
            catch(ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        private static bool IsValidSerial(int serial)
        {
            return serial is >= 0 and <= 999;
        }

        private static bool IsValidGenderIdentifier(int identifier)
        {
           return GetGenderFromGenderCode(identifier) != Gender.Unknown;
        }

        #endregion

        protected static PinData ValidatePinAndExtractParts(string thePin)
        {
            if (string.IsNullOrEmpty(thePin))
                throw new ArgumentNullException(nameof(thePin));

            var pinLength = thePin.Trim().Length;
            if (pinLength != 13)
            {
                throw new InvalidPinFormatException(
                    message: "Pin failed length requirement for Eswatini National ID.",
                    paramName: nameof(thePin)
                    );
            }

            var pinData = Extract(thePin);
            
            var isValid = IsValidDate(pinData.Year, pinData.Month, pinData.Day) &&
                IsValidGenderIdentifier(pinData.Gender) &&
                IsValidSerial(pinData.Serial);

            if (!isValid)
                throw new InvalidPinFormatException(
                    message: "Given PIN is not valid or in line with the format used for Eswatini National PINs.",
                    paramName: nameof(thePin)
                    );
            
            return pinData;
        }



        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            if (obj is not SwaziPin pin) return false;
            
            return pin.DateOfBirth.Equals(DateOfBirth) &&
                   pin.Gender.Equals(Gender) &&
                   pin.SerialNumber == SerialNumber;
        }

        public override string ToString()
        {
            return $"{DateOfBirth:yyMMdd}{_pinData.Gender}{SerialNumber:d3}";
        }

        /// <summary>
        /// Parses the given PIN string and creates a new PIN instance.
        /// </summary>
        /// <param name="pinToParse">The pin string to parse.</param>
        /// <returns>A PIN instance - might be invalid.</returns>
        /// <exception cref="ArgumentNullException">If string is null or empty</exception>
        /// <exception cref="InvalidPinFormatException">If pin is not valid format.</exception>
        public static SwaziPin Parse(string pinToParse)
        {
            if (string.IsNullOrWhiteSpace(pinToParse))
                throw new ArgumentNullException(nameof(pinToParse));
            
            return new SwaziPin(pinToParse.Trim());
        }

        public static bool TryParse(string pinToParse, out SwaziPin pin)
        {
            try
            {
                pin = Parse(pinToParse.Trim());
                return true;

            } catch (Exception)
            {
                pin = null!;
                return false;
            }
        }
        

        public static bool operator ==(SwaziPin left, SwaziPin right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SwaziPin left, SwaziPin right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DateOfBirth, Gender, SerialNumber);
        }
    }
    public record PinData(int Year, int Month, int Day, int Gender, int Serial);

    public sealed class InvalidNationalIdFormatException : FormatException
    {

        public InvalidNationalIdFormatException(string message) : base(message)
        {
            
        }

        public InvalidNationalIdFormatException(string message, Exception innerException) : base(message, innerException)
        {
            
        }

        public InvalidNationalIdFormatException(string message, string paramName = "")
        :base(message: string.IsNullOrWhiteSpace(paramName) ? message :
            $"{message}. Value given in {paramName}")
        {
            
        }
           
    }
    public sealed class InvalidPinFormatException(string message="", string paramName = "") : 
        ArgumentException(message: string.IsNullOrWhiteSpace(message) ?
                "Given PIN is not valid format for national PIN.": message,
            paramName: paramName) { }
    
}
