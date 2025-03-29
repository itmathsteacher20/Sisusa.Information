namespace Sisusa.Information.Communication
{
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

        public EswatiniTelNumber(uint phoneNumber):base(268, phoneNumber)
        {
            if (!IsValidNumber(phoneNumber))
            {
                throw new PhoneNumberOutOfRangeException(nameof(phoneNumber));
            }
        }
    }
    
    
}
