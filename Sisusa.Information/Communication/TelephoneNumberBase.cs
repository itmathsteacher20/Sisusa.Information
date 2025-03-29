namespace Sisusa.Information.Communication;

/// <summary>
/// The base telephone number class.
/// </summary>
public abstract class TelephoneNumberBase
{
    /// <summary>
    /// Generates a string representing the number in international phone number format.
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract string GetInInternationalNumberFormat();

    /// <summary>
    /// The telephone number in international number format -  country code is included.
    /// </summary>
    /// <returns>Representation of the telNumber in international format - with countrycode</returns>
    public abstract string InInternationalFormat();
    
    /// <summary>
    /// Generates a 'tel:' link that when clicked should dial the wrapped phone number.
    /// </summary>
    /// <returns>String representation of a 'tel:' link to dial the number.</returns>
    public abstract string GetCallLink();
        
    /// <summary>
    /// Gets the international phone number version of the Telephone number (if applicable).
    /// This format omits the + sign and combines the (countryCode)(areaCode)(subscriberNumber)
    /// to return a single long number.
    /// e.g. +268 2404 0001 will be returned as 26824040001.
    /// </summary>
    /// <returns>The international phone number version of the number.</returns>
    public abstract long GetLongPhoneNumber();
        
    
    public abstract override string ToString();
        
    /// <summary>
    /// Returns a formatted phone number where the phone number is displayed in groups of 4.
    /// i.e. +268 7612 3456 
    /// </summary>
    /// <returns>The formatted phone number.</returns>
    public abstract string  ToPrettyString();
}