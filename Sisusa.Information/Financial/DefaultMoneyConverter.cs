namespace Sisusa.Information.Financial;

public class DefaultMoneyConverter(Dictionary<(Currency, Currency), decimal> conversionRates) : IMoneyConverter
{
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