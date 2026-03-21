namespace Sisusa.Information.Communication
{
    /// <summary>
    /// Represents a telephone number in Eswatini, supporting validation for both landline and mobile formats.
    /// </summary>
    /// <remarks>This class ensures that the provided phone number conforms to the valid ranges for Eswatini
    /// landline and mobile numbers. It derives from the TelephoneNumber base class and automatically applies the
    /// Eswatini country code (268).</remarks>
    public class EswatiniTelNumber: TelephoneNumber
    {
        private static bool IsValidLandline(uint testNumber)
        {
            return testNumber is >= 22000000 and <= 25999999;
        }

        private static bool IsValidMobileNumber(uint testNumber) 
        {
            return testNumber is >= 76000000 and <= 79999999;
        }

        private static bool IsValidNumber(uint testNumber)
        {
            return IsValidLandline(testNumber) || IsValidMobileNumber(testNumber);
        }

        /// <summary>
        /// Initializes a new instance of the EswatiniTelNumber class with the specified phone number for the Eswatini
        /// country code.
        /// </summary>
        /// <param name="phoneNumber">The local phone number to associate with the Eswatini country code. Must be a valid Eswatini telephone
        /// number.</param>
        /// <exception cref="PhoneNumberOutOfRangeException">Thrown if phoneNumber is not a valid Eswatini telephone number.</exception>
        public EswatiniTelNumber(uint phoneNumber):base(268, phoneNumber)
        {
            if (!IsValidNumber(phoneNumber))
            {
                throw new PhoneNumberOutOfRangeException(nameof(phoneNumber));
            }
        }
    }
    
    
}
