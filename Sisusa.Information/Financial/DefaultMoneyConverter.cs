namespace Sisusa.Information.Financial;

/// <summary>
/// Converts money between different currencies using predefined conversion rates.
/// </summary>
/// <param name="conversionRates">A dictionary containing conversion rates between currency pairs.</param>
public class DefaultMoneyConverter(Dictionary<(Currency, Currency), decimal> conversionRates) : IMoneyConverter
{
    /// <summary>
    /// Converts the specified amount of money to the target currency.
    /// </summary>
    /// <param name="moneyToConvert">The money to convert.</param>
    /// <param name="targetCurrency">The currency to convert the money to.</param>
    /// <returns>A new <see cref="Money"/> instance in the target currency.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="moneyToConvert"/> or <paramref name="targetCurrency"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when a conversion rate between the specified currencies is not found.</exception>
    public Money Convert(Money moneyToConvert, Currency targetCurrency)
    {
        ArgumentNullException.ThrowIfNull(moneyToConvert);
        ArgumentNullException.ThrowIfNull(targetCurrency);

        if (moneyToConvert.Currency == targetCurrency)
        {
            return moneyToConvert;
        }

        if (conversionRates.TryGetValue((targetCurrency, moneyToConvert.Currency), out var rate))
        {
            var convertedCents = (int)Math.Round(moneyToConvert.InCents * rate);
            return new Money(convertedCents, targetCurrency);
        }
        throw new InvalidOperationException($"Cannot convert {moneyToConvert.Currency} to {targetCurrency}.");
    }
}
