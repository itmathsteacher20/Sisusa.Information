namespace Sisusa.Information.Communication;

/// <summary>
/// Provides methods for parsing email address strings.
/// </summary>
public static class EmailAddressParser
{
    /// <summary>
    /// Parses the specified email address string into an <see cref="EmailAddress"/> object.
    /// </summary>
    /// <param name="emailAddress">The email address string to parse.</param>
    /// <returns>An <see cref="EmailAddress"/> object.</returns>
    /// <exception cref="InvalidEmailAddressException">Thrown when the email address is invalid.</exception>
    public static EmailAddress Parse(string emailAddress)
    {
        EmailStringValidator.Validate(emailAddress);
        var emailParts = SplitToParts(emailAddress);
        return EmailAddress.BuildAddress
            .WithUserName(emailParts.localPart)
            .OnHost(emailParts.domainPart)
            .TryBuild();
    }

    private static (string localPart, string domainPart) SplitToParts(string emailAddress)
    {
        var emailParts = emailAddress.Split('@');
        return (emailParts[0], emailParts[1]);
    }
}