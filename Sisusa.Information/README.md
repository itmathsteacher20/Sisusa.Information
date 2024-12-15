# Sisusa Information Library

This library provides a comprehensive set of classes for handling various information types, including time, identification, financial, and communication data.

## Key Features:

    Robust Validation: Strict validation for all input data to ensure data integrity.
    Immutability: Most classes are immutable, promoting data consistency and preventing accidental modifications.
    Builder Pattern: Provides a fluent API for creating complex objects like addresses and contact information.
    Error Handling: Clear and informative exception handling for invalid input or unexpected scenarios.

### Core Classes:
#### Time

    Year: Represents a year with validation, comparison, and calculation capabilities.
    YearRange: Represents a range of years with inclusion checks and length calculations.
    DateRange: Represents a range of dates with inclusion checks and duration calculations.

#### Identification (Eswatini & South Africa Specific)

    SwaziPin: Parses and validates Eswatini national identification numbers (PINs), extracting date of birth, gender, and serial number.
    SAIdNumber: Parses and validates South African national identification numbers, extracting date of birth, gender, citizenship status and checkusm.
#### Financial

    Money: Represents a monetary amount with a specific currency. Supports operations like addition, subtraction, multiplication, comparison, and conversion to decimal.
    Currency: Enumeration for supported currencies (ZAR, USD, EUR).
    IMoneyConverter: Interface for converting money between currencies.
    DefaultMoneyConverter: A concrete implementation of IMoneyConverter that uses predefined conversion rates.
    LuhnChecksum: A simple implementation of the Luhn algorithm to validate numeric values (credit card numbers) using the Luhn algorithm.

#### Communication

    TelephoneNumber: Represents a phone number with country code and phone number.
    EswatiniTelNumber: Represents an Eswatini phone number with specific validation rules.
    EmailAddress: Represents an email address with validation and parsing capabilities.
    DetailedAddress: Represents a detailed address with country, city, street, nearest landmark, and postal code.
    ContactInformation: Combines phone number, email address, and physical address into a single class.

##### Usage:

For a detailed usage guide, refer to the specific class documentation within the library. Here's a basic example:
```C#

// Create a Swazi PIN and extract information
SwaziPin pin = SwaziPin.Parse("05071100245");
Console.WriteLine($"Gender: {pin.Gender}");
Console.WriteLine($"Date of Birth: {pin.DateOfBirth}");

//Create a South African PIN and extract information
SAIdNumber pin2 = SAIdNumber.Parse("9402285008081");
Console.WriteLine($"Citizenship: {pin2.Citizenship}");
Console.WriteLine($"Date of Birth: {pin2.DateOfBirth}");

SAIdNumber pin3 = SAIdNumber.Parse("9402285008080"); //invalid pin, throws exception


//Validate credit numbers using the Luhn algorithm
if (LuhnChecksum.IsValid(creditCardNumber))
{
    //proceed to payment 
} else {
    
    //display error or prompt user to enter valid card number
}

// Create a money object and perform calculations
Money amount = Money.FromParts(100, 50, Currency.ZAR);
Money increasedAmount = amount.IncrementByPercent(10);
Console.WriteLine($"Increased amount: {increasedAmount}");

// Create a contact information object
ContactInformation contact = ContactInformation.GetBuilder()
.WithPhoneNumber(TelephoneNumber.FromCountry(268).HavingPhoneNumber(76000000))
.WithEmailAddress(EmailAddress.Parse("john.doe@example.com"))
.AndLocatedAt(DetailedAddress.GetBuilder()
.InTownOrCity("Mbabane")
.WithNearestGeographicalMarker("Times Square")
.Create())
.Build();

Console.WriteLine($"Contact Information: {contact.PhoneNumber}, {contact.EmailAddress}, {contact.PhysicalAddress}");
```

Additional Notes:

    The library promotes a functional programming approach with immutable objects and pure functions.
    The validation and error handling mechanisms ensure data integrity and prevent invalid input.
    The builder pattern simplifies object creation and promotes a fluent API.
    The library is designed to be extensible and customizable to fit various use cases.

For more detailed usage instructions and examples, refer to the specific class documentation within the library.