namespace Sisusa.Information.Communication
{
    public class EswatiniTelNumber: TelephoneNumber
    {
        private static bool IsValidLandline(int testNumber)
        {
            return testNumber is >= 22000000 and <= 25999999;
        }

        private static bool IsValidMobileNumber(int testNumber) 
        {
            return testNumber is >= 76000000 and <= 79999999;
        }

        private static bool IsValidNumber(int testNumber)
        {
            return IsValidLandline(testNumber) || IsValidMobileNumber(testNumber);
        }

        public EswatiniTelNumber(int phoneNumber):base(268, phoneNumber)
        {
            if (!IsValidNumber(phoneNumber))
            {
                throw new PhoneNumberOutOfRangeException(nameof(phoneNumber));
            }
        }
        
        public EswatiniTelNumber(int phoneNumber, bool isEmergency=false):base(268, phoneNumber, isEmergency) 
        { }

        public static EswatiniTelNumber EmergencyNumber(int phoneNumber)
        {
            if (phoneNumber < 100 || phoneNumber > 9999)
            {
                //112, 911, 999, 9999, 933
                throw new PhoneNumberOutOfRangeException(nameof(phoneNumber));
            }
            return new EswatiniTelNumber(phoneNumber, true);
        }
    }
    
    
}
