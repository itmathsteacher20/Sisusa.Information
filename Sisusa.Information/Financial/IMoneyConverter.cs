namespace Sisusa.Information.Financial;

/// <summary>
/// Interface for converting money between different currencies.
/// </summary>
public interface IMoneyConverter
{
    /// <summary>
    /// Converts the specified money to the target currency.
    /// </summary>
    /// <param name="moneyToConvert">The money to convert.</param>
    /// <param name="targetCurrency">The target currency.</param>
    /// <returns>A new instance of <see cref="Money"/> in the target currency.</returns>
    public Money Convert(Money moneyToConvert, Currency targetCurrency);
}

