namespace Sisusa.Information.Financial;

/// <summary>
/// Provides methods to validate numbers using the Luhn checksum algorithm.
/// </summary>
public static class LuhnChecksum
{
    /// <summary>
    /// Validates if a given payload and checksum digit satisfy the Luhn checksum.
    /// </summary>
    /// <param name="payload">The numeric value excluding the checksum digit.</param>
    /// <param name="checksumDigit">The checksum digit to validate against.</param>
    /// <returns>True if the checksum digit is valid; otherwise, false.</returns>
    public static bool IsValid(long payload, int checksumDigit)
    {
        return checksumDigit == LuhnHelper.GetChecksumDigit(payload);
    }

    /// <summary>
    /// Validates if a given numeric value satisfies the Luhn checksum.
    /// </summary>
    /// <param name="valueToTest">The numeric value to test, including the checksum digit.</param>
    /// <returns>True if the numeric value satisfies the Luhn checksum; otherwise, false.</returns>
    public static bool IsValid(long valueToTest)
    {
        return LuhnHelper.GetChecksumDigit(valueToTest) == 0;
    }

    /// <summary>
    /// Generates a checksum digit for the given value.
    /// </summary>
    /// <param name="forValue">The value for which to generate a checksum</param>
    /// <returns>The digit which when appended to forValue would make the value valid under Luhn.</returns>
    public static int GenerateChecksum(long forValue)
    {
        return LuhnHelper.GetChecksumDigit(forValue);
    }
}

/// <summary>
/// Helper methods for calculating and validating Luhn checksums.
/// </summary>
public static class LuhnHelper
{
    /// <summary>
    /// Reverses the characters in a given string and returns them as a <see cref="Span{Char}"/>.
    /// </summary>
    /// <param name="payload">The string payload to reverse.</param>
    /// <returns>A span of characters in reverse order.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the payload is null.</exception>
    private static Span<char> GetInReverse(string payload)
    {
        return new Span<char>(
            payload
                .ToCharArray()
                .Reverse()
                .ToArray()
        );
    }

    /// <summary>
    /// Calculates the Luhn sum for a given numeric string payload.
    /// </summary>
    /// <param name="payload">The numeric string to calculate the Luhn sum for.</param>
    /// <returns>The Luhn sum of the numeric string.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the payload contains non-numeric characters.</exception>
    private static int GetSum(string payload)
    {
        var chars = GetInReverse(payload);
        var sumDoubled = 0;
        var sumSingled = 0;

        for (var i = 0; i < chars.Length; i++)
        {
            if (!char.IsDigit(chars[i]))
                throw new InvalidOperationException("Luhn algorithm only works on numeric values!");

            var digit = Convert.ToInt32(chars[i].ToString());
            if ((1 + i) % 2 == 0)
            {
                var doubled = digit * 2;
                sumDoubled += doubled > 9 ? (doubled - 9) : doubled;
            }
            else
            {
                sumSingled += digit;
            }
        }
        return sumSingled + sumDoubled;
    }

    /// <summary>
    /// Calculates the Luhn sum for a given numeric payload.
    /// </summary>
    /// <param name="payload">The numeric payload to calculate the Luhn sum for.</param>
    /// <returns>The Luhn sum of the numeric payload.</returns>
    /// <exception cref="FormatException">Thrown if the payload is negative.</exception>
    public static int GetSumFromPayload(long payload)
    {
        return payload switch
        {
            0 => 0,
            < 0 => throw new FormatException($"{payload} is not valid format for Luhn check."),
            _ => GetSum(payload.ToString())
        };
    }

    /// <summary>
    /// Calculates the checksum digit for a given numeric payload using the Luhn algorithm.
    /// </summary>
    /// <param name="payload">The numeric payload to calculate the checksum digit for.</param>
    /// <returns>The checksum digit required to make the payload valid under the Luhn algorithm.</returns>
    public static int GetChecksumDigit(long payload)
    {
        var presentSum = GetSumFromPayload(payload);
        if (presentSum % 10 == 0)
        {
            return 0;
        }
        if (presentSum < 10)
        {
            return 10 - presentSum;
        }

        var whole = presentSum / 10;
        var candidate = whole * 10;

        if (candidate < presentSum)
        {
            candidate = (whole + 1) * 10;
        }
        return candidate - presentSum;
    }
}