namespace Sisusa.Information.Communication;

/// <summary>
/// Exception thrown when an email provider format is invalid.
/// </summary>
public sealed class InvalidEmailProviderFormatException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailProviderFormatException"/> class.
    /// </summary>
    public InvalidEmailProviderFormatException()
        : base("Specified email provider (or hosting service) looks invalid.")
    {
    }
}