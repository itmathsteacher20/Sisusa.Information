# Sisusa Information Library - Communication

The library contains several classes related to communication information within the Sisusa Information Library. Here's a breakdown:

1. TelephoneNumber:

   Represents a phone number with country code and phone number.
   Ensures phone number format adheres to ITU E.164 standard.
   Provides methods for formatting, getting call link, and equality checks.
   Offers nested TelNumberBuilder for easier phone number creation.


2. EswatiniTelNumber:
   Inherits from TelephoneNumber and specifically handles phone numbers for Eswatini.
   Validates phone numbers based on specific ranges for landlines and mobile numbers.
   Has a factory method for emergency numbers with additional validation.


3. EmailAddress:
   Represents an email address with username and domain parts.
   Provides validation for email format and throws exceptions for invalid addresses.
   Offers static methods for parsing and creating email addresses.
   Includes a nested EmailAddressBuilder for constructing email addresses step-by-step.


4. EmailStringValidator:

   Validates email address strings based on predefined format rules.
   Throws exceptions for various invalid email formatting issues.


5. EmailAddressParser:

   Parses a valid email address string into its username and domain parts.


6. DetailedAddress:
   Represents a detailed address with country, city, street, nearest landmark, and postal code.
   Provides methods for equality checks and address building.
   Offers a nested AddressBuilder for constructing addresses with validations.

7. ContactInformation:
   Combines phone number, email address, and physical address into a single class.
   Provides access to individual address components.
   Offers a nested ContactInformationBuilder for constructing contact information objects.

These snippets demonstrate the Sisusa Information Library's focus on data integrity and well-structured classes for representing communication information.

## Usage Examples
