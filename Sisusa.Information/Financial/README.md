# Sisusa Information Library (Financial)

This library covers classes and functionalities for working with financial information, specifically money.

    Money: Represents an amount of money with a specific currency (ZAR, USD, EUR). Supports operations like addition, subtraction, multiplication, comparison, and conversion to decimal.
    Currency: Enumeration for supported currencies (ZAR - South African Rand, USD - US Dollar, EUR - Euro).
    IMoneyConverter: Interface for converting money between currencies.
    MoneyUtils: Provides utility methods related to money, such as validating cent values.
    MoneyParts: A struct representing money as separate whole and cent parts.

## Money Class:
### User Documentation: `Money` Class in the Sisusa.Information.Financial Namespace

The `Money` class represents monetary values in a specific currency and provides various operations for manipulating and comparing these values. This documentation explains the functionality of the `Money` class, its methods, operators, and related utilities, accompanied by practical examples.

## Class Overview

The `Money` class encapsulates a monetary value in terms of cents and its associated currency. It supports:

- Arithmetic operations (addition, subtraction, multiplication).
- Comparisons (equality, greater than, less than).
- Conversions and utilities.

### Properties

- **`InCents`**: (Read-only) The value of money in cents.
- **`Currency`**: (Read-only) The currency of the money (e.g., ZAR, USD).

### Static Members

- **`MinValue`**: Represents the smallest possible value (`0 ZAR`).
- **`Zero`**: Alias for `MinValue`.

### Methods

1. **`FromParts(int wholePart, int centPart, Currency currency = Currency.ZAR)`**
   Creates a `Money` instance from whole and fractional parts.

2. **`FromParts(MoneyParts parts, Currency currency = Currency.ZAR)`**
   Creates a `Money` instance from a `MoneyParts` struct.

3. **`ToDecimal()`**
   Converts the monetary value to a decimal representation.

4. **`IsDeficit()`**
   Determines if the monetary value is negative.

5. **`IncrementByPercent(double percent)`**
   Increases the monetary value by a percentage.

6. **`ReduceByPercent(double percent)`**
   Reduces the monetary value by a percentage.

### Operators

- **Arithmetic Operators**:
  - `+` : Adds two `Money` instances.
  - `-` : Subtracts one `Money` instance from another.
  - `*` : Multiplies `Money` by an integer or decimal.

- **Comparison Operators**:
  - `==` and `!=`: Check for equality or inequality.
  - `<`, `<=`, `>`, `>=`: Compare monetary values.

### Exception Handling

The class ensures:

- Currency mismatches during operations throw `InvalidOperationException`.
- Null arguments throw `ArgumentNullException`.

## Usage Examples

### 1. Creating a `Money` Instance

```csharp
var money = new Money(2500, Currency.USD); // $25.00
var moneyFromParts = Money.FromParts(25, 0, Currency.USD); // $25.00
```

### 2. Arithmetic Operations

#### Addition and Subtraction
```csharp
var money1 = new Money(2500, Currency.USD); // $25.00
var money2 = new Money(1000, Currency.USD); // $10.00

var sum = money1 + money2; // $35.00
var difference = money1 - money2; // $15.00
```

#### Multiplication
```csharp
var money = new Money(2500, Currency.USD); // $25.00

var multipliedByInt = money * 2; // $50.00
var multipliedByDecimal = money * 1.5m; // $37.50
```

### 3. Comparison

```csharp
var money1 = new Money(2500, Currency.USD); // $25.00
var money2 = new Money(1000, Currency.USD); // $10.00

bool isEqual = money1 == money2; // false
bool isGreater = money1 > money2; // true
```

### 4. Percentage Calculations

#### Increment by Percentage
```csharp
var money = new Money(2500, Currency.USD); // $25.00
var incremented = money.IncrementByPercent(10); // $27.50
```

#### Reduce by Percentage
```csharp
var money = new Money(2500, Currency.USD); // $25.00
var reduced = money.ReduceByPercent(20); // $20.00
```

### 5. Currency Validation

```csharp
var money1 = new Money(2500, Currency.USD);
var money2 = new Money(1500, Currency.ZAR);

try
{
    var invalidOperation = money1 + money2;
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message); // Cannot perform requested operation on monies of different currencies.
}
```

### 6. Conversion to Decimal

```csharp
var money = new Money(2599, Currency.USD); // $25.99
var decimalValue = money.ToDecimal(); // 25.99
```

### 7. Using `MoneyParts`

```csharp
var parts = new MoneyParts(25, 99);
var money = Money.FromParts(parts, Currency.USD); // $25.99
```

### 8. Equality Comparisons

```csharp
var money1 = new Money(2500, Currency.USD); // $25.00
var money2 = new Money(2500, Currency.USD); // $25.00

bool areEqual = money1.Equals(money2); // true
```

### 9. String Representation

```csharp
var money = new Money(2500, Currency.USD); // $25.00
Console.WriteLine(money.ToString()); // "USD 25.00"
```

## Supporting Components

### `Currency` Enum

Defines supported currencies:
- `ZAR`: South African Rand
- `USD`: United States Dollar
- `EUR`: Euro

### `IMoneyConverter` Interface

A contract for currency conversion. Example:

```csharp
public class MoneyConverter : IMoneyConverter
{
    public Money Convert(Money moneyToConvert, Currency targetCurrency)
    {
        // Conversion logic
        return new Money((int)(moneyToConvert.InCents * 1.5), targetCurrency);
    }
}
```

### `MoneyUtils` Static Class

Utility for monetary validations.

- **`IsValidCents(int cents)`**: Validates if the cents value is within the range 0 to 99.

### `MoneyParts` Struct

Encapsulates the whole and cent parts of a monetary value, ensuring cent values are valid.

```csharp
var parts = new MoneyParts(25, 99); // Whole part: 25, Cents: 99
```

## Summary

The `Money` class is a robust tool for representing and manipulating monetary values with precision and safety. By following its API and handling exceptions properly, you can integrate it seamlessly into financial applications.

# User Documentation for DefaultMoneyConverter

## Overview
The `DefaultMoneyConverter` class is designed to provide a simple and efficient way to handle currency conversion. It implements the `IMoneyConverter` interface and uses predefined exchange rates to convert monetary values between different currencies.

### Key Features:
- Implements `IMoneyConverter` for consistent currency conversion.
- Uses a dictionary to store exchange rates for efficient lookups.
- Supports conversion between different currencies or returns the original `Money` object if the target currency is the same as the source currency.
- Throws exceptions when conversions are not possible due to missing exchange rates.

---

## Constructor
### `DefaultMoneyConverter(Dictionary<(Currency, Currency), decimal> conversionRates)`
Initializes the `DefaultMoneyConverter` with a dictionary of conversion rates.

#### Parameters:
- `conversionRates`: A dictionary where each key is a tuple of two `Currency` values representing the source and target currencies, and the value is the exchange rate.

#### Example:
```csharp
var rates = new Dictionary<(Currency, Currency), decimal>
{
    {(Currency.USD, Currency.ZAR), 15.50m},
    {(Currency.ZAR, Currency.USD), 0.0645m},
    {(Currency.EUR, Currency.USD), 1.10m},
    {(Currency.USD, Currency.EUR), 0.91m}
};
var converter = new DefaultMoneyConverter(rates);
```

---

## Methods

### `Convert(Money moneyToConvert, Currency targetCurrency)`
Converts the given `Money` object to the specified target currency using the predefined exchange rates.

#### Parameters:
- `moneyToConvert`: The `Money` object to convert.
- `targetCurrency`: The target `Currency` to which the money should be converted.

#### Returns:
- A new `Money` object in the target currency.

#### Exceptions:
- `ArgumentNullException`: Thrown if `moneyToConvert` or `targetCurrency` is null.
- `InvalidOperationException`: Thrown if no exchange rate exists for the conversion.

#### Example:
```csharp
// Given conversion rates:
var rates = new Dictionary<(Currency, Currency), decimal>
{
    {(Currency.USD, Currency.ZAR), 15.50m},
    {(Currency.ZAR, Currency.USD), 0.0645m}
};

var converter = new DefaultMoneyConverter(rates);

// Create a Money object:
var usdAmount = new Money(10000, Currency.USD); // $100.00

// Convert to ZAR:
var zarAmount = converter.Convert(usdAmount, Currency.ZAR);
Console.WriteLine(zarAmount); // Outputs: "ZAR 1550.00"

// Convert back to USD:
var convertedBackToUsd = converter.Convert(zarAmount, Currency.USD);
Console.WriteLine(convertedBackToUsd); // Outputs: "USD 100.00"
```

---

## Error Handling
1. **Handling Missing Conversion Rates**:
   If a conversion rate is missing for the specified currencies, an `InvalidOperationException` is thrown with a message indicating the unsupported conversion.

#### Example:
```csharp
try
{
    var zarAmount = converter.Convert(usdAmount, Currency.EUR);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message); // Outputs: "Cannot convert USD to EUR."
}
```

2. **Null Arguments**:
   If either `moneyToConvert` or `targetCurrency` is null, an `ArgumentNullException` is thrown.

#### Example:
```csharp
try
{
    var nullResult = converter.Convert(null, Currency.ZAR);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine(ex.Message); // Outputs an error message about null arguments.
}
```

---

## Real-World Use Cases
1. **E-commerce Applications**:
   Convert product prices displayed in the user's local currency to another currency for international customers.

2. **Financial Software**:
   Calculate and display account balances or transaction amounts in multiple currencies.

3. **Travel Applications**:
   Provide users with estimates of currency exchange for travel budgets or transaction conversions.

#### Example:
```csharp
var travelRates = new Dictionary<(Currency, Currency), decimal>
{
    {(Currency.USD, Currency.EUR), 0.91m},
    {(Currency.EUR, Currency.USD), 1.10m}
};

var travelConverter = new DefaultMoneyConverter(travelRates);
var euros = new Money(50000, Currency.EUR); // €500.00

var usd = travelConverter.Convert(euros, Currency.USD);
Console.WriteLine(usd); // Outputs: "USD 550.00"
```

---

## Summary
The `DefaultMoneyConverter` class is a highly flexible and robust solution for currency conversion. By leveraging a dictionary of exchange rates, it ensures efficient and reliable conversion operations. Its straightforward design makes it ideal for various financial and business applications where currency conversion is needed.

---

## Additional Notes
- Ensure the conversion rates dictionary is up-to-date for accurate results.
- Consider wrapping `DefaultMoneyConverter` in a service that periodically updates exchange rates from a reliable API.

# LuhnChecksum and LuhnHelper User Documentation

## Overview
The `LuhnChecksum` and `LuhnHelper` classes provide methods to calculate, validate, and generate checksum digits using the **Luhn algorithm**. This algorithm is widely used for validating identification numbers such as credit card numbers and other numerical identifiers.

## Namespace
`Sisusa.Information.Financial`

---

## Classes

### 1. `LuhnChecksum`
This class provides static methods to:
- Validate numeric values using the Luhn checksum.
- Generate a checksum digit for a given numeric payload.

#### Methods

**`IsValid(long payload, int checksumDigit)`**
- **Description**: Validates if a given payload and checksum digit satisfy the Luhn checksum.
- **Parameters**:
  - `payload`: The numeric value excluding the checksum digit.
  - `checksumDigit`: The checksum digit to validate against.
- **Returns**: `true` if the checksum digit is valid; otherwise, `false`.
- **Example**:
  ```csharp
  long payload = 123456;
  int checksumDigit = 4;
  bool isValid = LuhnChecksum.IsValid(payload, checksumDigit);
  Console.WriteLine(isValid); // Output: true or false
  ```

**`IsValid(long valueToTest)`**
- **Description**: Validates if a given numeric value, including the checksum digit, satisfies the Luhn checksum.
- **Parameters**:
  - `valueToTest`: The numeric value to test, including the checksum digit.
- **Returns**: `true` if the value satisfies the Luhn checksum; otherwise, `false`.
- **Example**:
  ```csharp
  long valueToTest = 1234564;
  bool isValid = LuhnChecksum.IsValid(valueToTest);
  Console.WriteLine(isValid); // Output: true or false
  ```

**`GenerateChecksum(long forValue)`**
- **Description**: Generates a checksum digit for a given numeric payload.
- **Parameters**:
  - `forValue`: The numeric value for which to generate a checksum digit.
- **Returns**: The checksum digit required to make the payload valid under the Luhn algorithm.
- **Example**:
  ```csharp
  long forValue = 123456;
  int checksum = LuhnChecksum.GenerateChecksum(forValue);
  Console.WriteLine(checksum); // Output: 4 (or another checksum digit)
  ```

---

### 2. `LuhnHelper`
This helper class provides utility methods to:
- Calculate the Luhn sum of numeric values.
- Reverse characters in a string (for internal use).
- Generate checksum digits for numeric values.

#### Methods

**`GetInReverse(string payload)`**
- **Description**: Reverses the characters in a given string.
- **Parameters**:
  - `payload`: The string payload to reverse.
- **Returns**: A `Span<char>` containing the reversed characters.
- **Throws**: `ArgumentNullException` if `payload` is null.
- **Note**: This method is private and intended for internal use.

**`GetSum(string payload)`**
- **Description**: Calculates the Luhn sum for a numeric string payload.
- **Parameters**:
  - `payload`: The numeric string to calculate the Luhn sum for.
- **Returns**: The Luhn sum of the numeric string.
- **Throws**:
  - `InvalidOperationException` if `payload` contains non-numeric characters.
- **Example**:
  ```csharp
  // Internal usage, accessed via public methods
  ```

**`GetSumFromPayload(long payload)`**
- **Description**: Calculates the Luhn sum for a numeric payload.
- **Parameters**:
  - `payload`: The numeric payload to calculate the Luhn sum for.
- **Returns**: The Luhn sum of the numeric payload.
- **Throws**:
  - `FormatException` if `payload` is negative.
- **Example**:
  ```csharp
  long payload = 123456;
  int luhnSum = LuhnHelper.GetSumFromPayload(payload);
  Console.WriteLine(luhnSum); // Output: Luhn sum
  ```

**`GetChecksumDigit(long payload)`**
- **Description**: Calculates the checksum digit for a given numeric payload.
- **Parameters**:
  - `payload`: The numeric payload to calculate the checksum digit for.
- **Returns**: The checksum digit required to make the payload valid under the Luhn algorithm.
- **Example**:
  ```csharp
  long payload = 123456;
  int checksumDigit = LuhnHelper.GetChecksumDigit(payload);
  Console.WriteLine(checksumDigit); // Output: 4
  ```

---

## Real-World Usage Examples

### Example 1: Validate a Credit Card Number
```csharp
long cardNumber = 4111111111111111;
bool isValid = LuhnChecksum.IsValid(cardNumber);
Console.WriteLine(isValid ? "Valid card" : "Invalid card");
```

### Example 2: Generate a Checksum for an Identification Number
```csharp
long idWithoutChecksum = 123456;
int checksum = LuhnChecksum.GenerateChecksum(idWithoutChecksum);
Console.WriteLine($"Checksum for {idWithoutChecksum}: {checksum}");
```

### Example 3: Validate a Custom Identification Number
```csharp
long payload = 987654;
int checksumDigit = LuhnChecksum.GenerateChecksum(payload);
bool isValid = LuhnChecksum.IsValid(payload, checksumDigit);
Console.WriteLine(isValid ? "Valid ID" : "Invalid ID");
```

### Example 4: Handling Invalid Inputs
```csharp
try
{
    long invalidPayload = -123456;
    int checksum = LuhnChecksum.GenerateChecksum(invalidPayload);
}
catch (FormatException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

---

## Design Notes
- **Static Methods**: Both `LuhnChecksum` and `LuhnHelper` use static methods for utility purposes, making the classes stateless and thread-safe.
- **Error Handling**: Comprehensive error handling ensures robustness, such as validating numeric inputs and throwing exceptions for invalid formats.
- **Private Methods**: Internal helper methods like `GetInReverse` and `GetSum` encapsulate complexity, keeping public methods clean and focused.

---

## Known Limitations
- The `GetInReverse` method converts the string to a `Span<char>` via intermediate array operations, which might not be optimal for extremely large strings.
- Methods expect numeric inputs and will throw exceptions for non-numeric or negative values. Input validation should be performed by the caller.

---

This documentation should help developers understand and use the Luhn checksum utility effectively in various applications.






















This is a basic overview of the financial functionalities in the Sisusa Information Library. Refer to the code documentation for detailed explanations and additional features.