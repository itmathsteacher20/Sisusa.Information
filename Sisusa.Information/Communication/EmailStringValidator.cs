using System.Text.RegularExpressions;

namespace Sisusa.Information.Communication;

/// <summary>
/// Provides methods for validating email address strings.
/// </summary>
public static class EmailStringValidator
{
    private static readonly Regex EmailFormat = new("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");

    /// <summary>
    /// Validates the specified email address string.
    /// </summary>
    /// <param name="emailAddress">The email address string to validate.</param>
    /// <exception cref="InvalidEmailAddressException">Thrown when the email address is invalid.</exception>
    public static void Validate(string emailAddress)
    {
        ThrowIfNullOrEmpty(emailAddress, "Email address to validate cannot be empty.");
        ThrowIfContainsInvalidCharacters(emailAddress, "Given string is not a valid email address (for this application).");

        var noOfAtChars = emailAddress.Count(c => c == '@');

        if (noOfAtChars is 0 or > 1)
            throw new InvalidEmailAddressException(
                "Email address can have only one @ character separating localPart from domainPart.");

        char lastChar = char.MinValue;
        foreach (char c in emailAddress)
        {
            if (c == '.' && lastChar == '.')
            {
                throw new InvalidEmailAddressException("Email address cannot contain consecutive dots.");
            }
            lastChar = c;
        }

        if (!EmailFormat.IsMatch(emailAddress))
            throw new InvalidEmailAddressException();
    }

    private static void ThrowIfNullOrEmpty(string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailAddressException(message);
        }
    }

    private static void ThrowIfContainsInvalidCharacters(string value, string message)
    {
        if (value.StartsWith('.') || value.EndsWith('.'))
        {
            throw new InvalidEmailAddressException(message);
        }

        if (value.StartsWith('@') || value.EndsWith('@'))
        {
            throw new InvalidEmailAddressException(message);
        }

        if (value.Contains(' '))
        {
            throw new InvalidEmailAddressException(message);
        }

        if (value.Contains('"'))
        {
            throw new InvalidEmailAddressException(message);
        }
    }
}