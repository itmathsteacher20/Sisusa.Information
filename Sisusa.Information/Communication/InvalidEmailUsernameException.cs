namespace Sisusa.Information.Communication;

/// <summary>
/// Exception thrown when an email username is invalid.
/// </summary>
public sealed class InvalidEmailUsernameException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailUsernameException"/> class.
    /// </summary>
    public InvalidEmailUsernameException()
        : base("Invalid email format - user part contains invalid characters.")
    {
    }
}