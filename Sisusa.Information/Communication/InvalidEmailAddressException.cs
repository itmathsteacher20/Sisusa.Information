namespace Sisusa.Information.Communication;

/// <summary>
/// Exception thrown when an email address is invalid.
/// </summary>
public sealed class InvalidEmailAddressException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailAddressException"/> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public InvalidEmailAddressException(string message = "Provided string does not appear to be a valid email address.")
        : base(message)
    {
    }
}