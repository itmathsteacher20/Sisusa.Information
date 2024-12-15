//using Sisusa.People;
//using Sisusa.Sisusa.Information.Communication;
namespace Sisusa.Information.Communication
{
    public class ContactInformation(
        TelephoneNumber phoneNumber,
        EmailAddress emailAddress,
        DetailedAddress physicalAddress)
    {
        public TelephoneNumber PhoneNumber { get; init; } = phoneNumber;
        
        public EmailAddress EmailAddress { get; init; } = emailAddress;
        
        public DetailedAddress PhysicalAddress { get; init; } = physicalAddress;

        public override int GetHashCode()
        {
            return HashCode.Combine(PhoneNumber, EmailAddress, PhysicalAddress);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is ContactInformation contactInformation)
            {
                return PhoneNumber == contactInformation.PhoneNumber && 
                       EmailAddress == contactInformation.EmailAddress &&
                       PhysicalAddress == contactInformation.PhysicalAddress;
            }
            return false;
        }

        public static bool operator ==(ContactInformation left, ContactInformation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ContactInformation left, ContactInformation right)
        {
            return !Equals(left, right);
        }
        
        public static ContactInformationBuilder GetBuilder() => new ContactInformationBuilder();
        

        public sealed class ContactInformationBuilder
        {
#pragma warning disable CS8618
            private TelephoneNumber _phoneNumber;
            private EmailAddress _emailAddress;
            private DetailedAddress _physicalAddress;
#pragma warning restore CS8618
            //withPhone().withEmail().andLocatedAt()

            public ContactInformationBuilder WithPhoneNumber(TelephoneNumber phoneNumber)
            {
                _phoneNumber = phoneNumber;
                return this;
            }

            public ContactInformationBuilder WithEmailAddress(EmailAddress emailAddress)
            {
                _emailAddress = emailAddress;
                return this;
            }

            public ContactInformationBuilder AndLocatedAt(DetailedAddress physicalAddress)
            {
                _physicalAddress = physicalAddress;
                return this;
            }

            public ContactInformation Build()
            {
                return new ContactInformation(
                    _phoneNumber,
                    _emailAddress,
                    _physicalAddress);
            }
        }
    }
}
