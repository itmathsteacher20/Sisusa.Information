using Sisusa.Information.Financial;

namespace Sisusa.Information.Identification
{
    /// <summary>
    /// Represents a South African ID number and provides validation and extraction of its components.
    /// </summary>
    public class SAIdNumber 
    {
        private readonly PinData _pinParts;
        private readonly SouthAfricanIdInfo _pinInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SAIdNumber"/> class.
        /// </summary>
        /// <param name="thePin">The South African ID number as a string.</param>
        /// <exception cref="InvalidPinFormatException">Thrown when the provided PIN is not valid.</exception>
        private SAIdNumber(string thePin) 
        {  
            if (!LuhnChecksum.IsValid(long.Parse(thePin)))
                throw new InvalidPinFormatException(
                    message: $"Given value `{thePin}` is not in line with format used for South African National ID numbers.",
                    paramName: nameof(thePin)
                );
            _pinParts = ValidatePinAndExtractParts(thePin);
            _pinInfo = GetPinInfo(_pinParts.Serial);
        }

        private static PinData Extract(string thePin)
        {
            int year = int.Parse(thePin[..2]);
            int month = int.Parse(thePin.Substring(2, 2));
            int day = int.Parse(thePin.Substring(4, 2));
            int gender = int.Parse(thePin.Substring(6, 4));
            int serial = int.Parse(thePin.Substring(10, 3));

            if (year <= 26)
            {
                year = 2000 + year;
            } else
            {
                year = 1900 + year;
            }
            return new PinData(year, month, day, gender, serial);
        }

        private static bool IsValidDate(int year, int month, int day)
        {
            try
            {
                _ = new DateTime(year, month, day);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidSerial(int serial)
        {
            return serial is > 0 and < 1000;
        }

        /// <summary>
        /// Validates the provided South African ID number.
        /// </summary>
        /// <param name="thePin">The South African ID number to validate.</param>
        /// <returns>Extracted Pin parts on successful validation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided PIN is null or empty.</exception>
        /// <exception cref="InvalidPinFormatException">Thrown when the provided PIN does not meet format requirements.</exception>
        private static PinData ValidatePinAndExtractParts(string thePin)
        {
            if (string.IsNullOrEmpty(thePin))
                throw new ArgumentNullException(nameof(thePin));

            if (thePin.Trim().Length != 13)
                throw new InvalidPinFormatException(
                    message: "Given pin value failed length requirement for SA National ID number.",
                    paramName: nameof(thePin)
                );

            if (!LuhnChecksum.IsValid(long.Parse(thePin)))
                throw new InvalidPinFormatException(
                    message: "Specified PIN is not valid - checksum failed.",
                    paramName: nameof(thePin)
                );

            var theParts = Extract(thePin);
            var validParts = IsValidDate(theParts.Year, theParts.Month, theParts.Day) &&
                             SAIdNumber.IsValidGenderIdentifier(theParts.Gender) &&
                             IsValidSerial(theParts.Serial);
            if (!validParts)
                throw new InvalidPinFormatException(
                    message: "Specified PIN (DGS) is not valid format for SA National ID number.",
                    paramName: nameof(thePin));
            
            var saInfo = GetPinInfo(theParts.Serial);
            
            if (saInfo.Citizenship is < 0 or > 2)
                throw new InvalidPinFormatException(
                    message: $"Specified PIN (CZ) {saInfo.Citizenship} is not valid format for SA National ID number.",
                    paramName: nameof(thePin)
                );

            if (saInfo.Spacer > 9)
            {
                throw new InvalidPinFormatException(
                    message: "Invalid PIN Format for SA National ID number.",
                    nameof(thePin)
                );
            }

            return theParts;
        }

        /// <summary>
        /// Retrieves information from the given PIN info.
        /// </summary>
        /// <param name="pinInfo">The PIN info to extract data from.</param>
        /// <returns>A <see cref="SouthAfricanIdInfo"/> record containing citizenship, spacer, and checksum.</returns>
        /// <exception cref="InvalidPinFormatException">Thrown when the specified PIN info is invalid.</exception>
        private static SouthAfricanIdInfo GetPinInfo(int pinInfo)
        {
            if (pinInfo > 299)
                throw new InvalidPinFormatException(
                    message: "Specified PIN (CAZ) is not valid format for SA National ID number.",
                    paramName: nameof(pinInfo)
                );
            var infoStr = pinInfo.ToString("d3");
            
            var citizenship = int.Parse(infoStr[..1]);
            var holder = int.Parse(infoStr[^2].ToString());
            var checksum = int.Parse(infoStr[^1].ToString());
           

            return new SouthAfricanIdInfo(citizenship, holder, checksum);
        }

        /// <summary>
        /// Validates the gender identifier extracted from the ID number.
        /// </summary>
        /// <param name="genderIdentifier">The gender identifier to validate.</param>
        /// <returns><c>true</c> if the gender identifier is valid; otherwise, <c>false</c>.</returns>
        private static bool IsValidGenderIdentifier(int genderIdentifier)
        {
            return GetGenderFromId(genderIdentifier) != Gender.Unknown;
        }

        /// <summary>
        /// Gets the gender based on the provided gender ID.
        /// </summary>
        /// <param name="genderId">The gender ID.</param>
        /// <returns>The <see cref="Gender"/> associated with the provided ID.</returns>
        private static Gender GetGenderFromId(int genderId)
        {
            return genderId switch
            {
                >= 0 and <= 4999 => Gender.Female,
                >= 5000 and <= 9999 => Gender.Male,
                _ => Gender.Unknown
            };
        }

        /// <summary>
        /// Gets the citizenship status based on the provided PIN information.
        /// </summary>
        /// <param name="pinInfo">The PIN information.</param>
        /// <returns>The <see cref="CitizenshipStatus"/> associated with the PIN info.</returns>
        private static CitizenshipStatus GetCitizenship(SouthAfricanIdInfo pinInfo)
        {
            return pinInfo.Citizenship switch
            {
                0 => CitizenshipStatus.FullCitizen,
                1 => CitizenshipStatus.PermanentResident,
                2 => CitizenshipStatus.Refugee,
                _ => CitizenshipStatus.Unknown
            };
        }

        private static DateOnly GetDateFromParts(PinData pinParts)
        {
            return !IsValidDate(pinParts.Year, pinParts.Month, pinParts.Day) ?
                DateOnly.MinValue : 
                DateOnly.FromDateTime(new DateTime(pinParts.Year, pinParts.Month, pinParts.Day));
        }

        /// <summary>
        /// Gets the gender of the ID holder.
        /// </summary>
        public Gender Gender => GetGenderFromId(_pinParts.Gender);

        /// <summary>
        /// Gets the citizenship status of the ID holder.
        /// </summary>
        public CitizenshipStatus CitizenshipStatus => GetCitizenship(_pinInfo);

        /// <summary>
        /// Gets the serial number derived from the ID number.
        /// </summary>
        private int SerialNumber => (_pinInfo.Spacer * 10) + _pinInfo.Checksum;

        public DateOnly DateOfBirth => GetDateFromParts(_pinParts);
        
        /// <summary>
        /// Parses a string representation of a South African ID number and returns an instance of <see cref="SAIdNumber"/>.
        /// </summary>
        /// <param name="pinToParse">The string representation of the ID number.</param>
        /// <returns>An instance of <see cref="SAIdNumber"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the provided PIN is null or empty.</exception>
        /// <exception cref="InvalidPinFormatException">Thrown when the provided PIN is invalid.</exception>
        public static SAIdNumber Parse(string pinToParse)
        {
            if (string.IsNullOrWhiteSpace(pinToParse))
                throw new ArgumentNullException(nameof(pinToParse));
            
            return new SAIdNumber(pinToParse.Trim());
        }

        /// <summary>
        /// Tries to parse a string representation of a South African ID number and returns an instance of <see cref="SAIdNumber"/>.
        /// </summary>
        /// <param name="pinToParse">The string representation of the ID number.</param>
        /// <param name="pin">When this method returns, contains the parsed <see cref="SAIdNumber"/> if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
        public static bool TryParse(string pinToParse, out SAIdNumber pin)
        {
            try
            {
                pin = Parse(pinToParse.Trim());
                return true;
            }
            catch
            {
                pin = null!;
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified object is equal to the current instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is SAIdNumber saId)
            {
                return AreEqual(this, saId);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="SAIdNumber"/> is equal to the current instance.
        /// </summary>
        /// <param name="other">The <see cref="SAIdNumber"/> to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="SAIdNumber"/> is equal to the current instance; otherwise, <c>false</c>.</returns>
        public bool Equals(SAIdNumber other)
        {
            return AreEqual(this, other);
        }

        private static bool AreEqual(SAIdNumber first, SAIdNumber second)
        {
            if (ReferenceEquals(first, second))
                return true;
            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
                return false;
            
            return first.DateOfBirth.Equals(second.DateOfBirth) &&
                   first.SerialNumber == second.SerialNumber &&
                   first.Gender == second.Gender &&
                   first.CitizenshipStatus == second.CitizenshipStatus;
        }

        /// <summary>
        /// Returns a hash code for the current instance.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(DateOfBirth, SerialNumber, Gender, CitizenshipStatus);
        }

        /// <summary>
        /// Returns a string representation of the current instance.
        /// </summary>
        /// <returns>A string that represents the current instance.</returns>
        public override string ToString()
        {
            return $"{DateOfBirth:yyMMdd}{(_pinParts.Gender):d4}{(ushort)CitizenshipStatus}{SerialNumber:d2}";
        }

        public static bool operator ==(SAIdNumber a, SAIdNumber b)
        {
            if (ReferenceEquals(a, b))
                return true;
            return !ReferenceEquals(a, null) && a.Equals(b);
        }

        public static bool operator !=(SAIdNumber a, SAIdNumber b)
        {
            return !(a == b);
        }
    }

    /// <summary>
    /// Represents information extracted from a South African ID number.
    /// </summary>
    /// <param name="Citizenship">The citizenship status.</param>
    /// <param name="Spacer">The spacer value.</param>
    /// <param name="Checksum">The checksum value.</param>
    public record SouthAfricanIdInfo(int Citizenship, int Spacer, int Checksum);

    /// <summary>
    /// Represents the citizenship status of the ID holder.
    /// </summary>
    public enum CitizenshipStatus : ushort
    {
        /// <summary>
        /// Full citizen of South Africa.
        /// </summary>
        FullCitizen = 0,
        
        /// <summary>
        /// Permanent resident of South Africa.
        /// </summary>
        PermanentResident = 1,
        
        /// <summary>
        /// Refugee status in South Africa.
        /// </summary>
        Refugee = 2,
        
        /// <summary>
        /// Unknown citizenship status.
        /// </summary>
        Unknown = 3
    }
}
