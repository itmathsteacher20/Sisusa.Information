namespace Sisusa.Information.Communication;

public abstract class TelephoneNumberBase
{
    public abstract string GetInInternationalNumberFormat();
    public abstract string GetCallLink();
        
    public abstract long GetLongPhoneNumber();
        
    public abstract override string ToString();
        
    public abstract string  ToPrettyString();
}