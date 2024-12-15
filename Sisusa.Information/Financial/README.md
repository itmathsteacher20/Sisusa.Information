# Sisusa Information Library (Financial)

This library covers classes and functionalities for working with financial information, specifically money.

    Money: Represents an amount of money with a specific currency (ZAR, USD, EUR). Supports operations like addition, subtraction, multiplication, comparison, and conversion to decimal.
    Currency: Enumeration for supported currencies (ZAR - South African Rand, USD - US Dollar, EUR - Euro).
    IMoneyConverter: Interface for converting money between currencies.
    MoneyUtils: Provides utility methods related to money, such as validating cent values.
    MoneyParts: A struct representing money as separate whole and cent parts.

## Money Class:

The Money class represents a monetary value with a specific currency. You can create a Money object by specifying the amount in cents and the desired currency. It provides various methods for calculations and formatting:

    ToDecimal(): Converts the money amount to a decimal value.
    IsDeficit(): Checks if the money amount is negative (a deficit).
    IncrementByPercent(percent): Increases the money amount by a specified percentage.
    ReduceByPercent(percent): Decreases the money amount by a specified percentage.
    ToString(): Formats the money amount as a string with currency symbol and two decimal places.

## Equality and Comparison:

The `Money` class allows comparisons between money objects and checks for equality based on both the amount (cents) and currency.

### Operators:

The `Money` class supports various arithmetic operators for addition, subtraction, and multiplication with other Money objects and integer/decimal values. However, these operations are only allowed for money objects with the same currency.

### Money Conversion:

The `IMoneyConverter` interface defines a way to convert money between different currencies. For actual conversion, you can use the DefaultMoneyConverter class that requires a dictionary of conversion rates between supported currencies.

Example Usage:
```C#

Money amount = Money.FromParts(100, 50, Currency.ZAR); // R100.50

Console.WriteLine($"Amount in Decimal: {amount.ToDecimal():C2}"); // Outputs: R100.50

Money increasedAmount = amount.IncrementByPercent(10); // R110.55
Money difference = increasedAmount - amount; // R10.05

if (increasedAmount > amount)
{
Console.WriteLine("Increased amount is greater.");
}

Money convertedAmount = new DefaultMoneyConverter(conversionRates).Convert(amount, Currency.USD); // Assuming conversionRates dictionary is populated

Console.WriteLine($"Converted amount: {convertedAmount}"); // Outputs converted amount in USD
```


This is a basic overview of the financial functionalities in the Sisusa Information Library. Refer to the code documentation for detailed explanations and additional features.