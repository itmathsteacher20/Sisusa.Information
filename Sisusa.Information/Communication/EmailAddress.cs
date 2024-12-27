using System.Text.RegularExpressions;

namespace Sisusa.Information.Communication
{
    /// <summary>
    /// Represents an email address with user part and host part.
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Represents a stub email address.
        /// </summary>
        public static readonly EmailAddress None = new("stub", "domain.tld");

        /// <summary>
        /// The user part of the email address i.e. the part before @.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// The email provider - part after the @.
        /// </summary>
        public string EmailHost { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        /// <param name="userPart">The user part of the email address.</param>
        /// <param name="domainName">The domain part of the email address.</param>
        private EmailAddress(string userPart, string domainName)
        {
            UserName = userPart;
            EmailHost = domainName;
        }

        /// <summary>
        /// Gets a builder for creating an <see cref="EmailAddress"/>.
        /// </summary>
        public static EmailAddressBuilder BuildAddress => new();

        /// <summary>
        /// Parses the specified email address string into an <see cref="EmailAddress"/> object.
        /// </summary>
        /// <param name="emailAddress">The email address string to parse.</param>
        /// <returns>An <see cref="EmailAddress"/> object.</returns>
        /// <exception cref="InvalidEmailAddressException">Thrown when the email address is invalid.</exception>
        public static EmailAddress Parse(string emailAddress)
        {
            EmailStringValidator.Validate(emailAddress);
            return EmailAddressParser.Parse(emailAddress);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return HashCode.Combine(UserName, EmailHost);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{UserName}@{EmailHost}";
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;

            if (obj is EmailAddress emailAddress)
            {
                return string.Equals(UserName, emailAddress.UserName, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(EmailHost, emailAddress.EmailHost, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="EmailAddress"/> are equal.
        /// </summary>
        /// <param name="a">The first <see cref="EmailAddress"/> to compare.</param>
        /// <param name="b">The second <see cref="EmailAddress"/> to compare.</param>
        /// <returns>true if the two <see cref="EmailAddress"/> instances are equal; otherwise, false.</returns>
        public static bool operator ==(EmailAddress? a, EmailAddress b)
        {
            if (ReferenceEquals(a, b)) 
                return true;

            if (a is null || ReferenceEquals(a, null))
                return b is null || ReferenceEquals(b, null);
           
            return a.Equals(b);
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="EmailAddress"/> are not equal.
        /// </summary>
        /// <param name="a">The first <see cref="EmailAddress"/> to compare.</param>
        /// <param name="b">The second <see cref="EmailAddress"/> to compare.</param>
        /// <returns>true if the two <see cref="EmailAddress"/> instances are not equal; otherwise, false.</returns>
        public static bool operator !=(EmailAddress? a, EmailAddress b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Implicitly converts an <see cref="EmailAddress"/> to a string.
        /// </summary>
        /// <param name="emailAddress">The <see cref="EmailAddress"/> to convert.</param>
        /// <returns>The string representation of the <see cref="EmailAddress"/>.</returns>
        public static implicit operator string(EmailAddress emailAddress)
        {
            return emailAddress is null ? string.Empty : emailAddress.ToString();
        }

        /// <summary>
        /// Builder class for constructing <see cref="EmailAddress"/> instances.
        /// </summary>
        public class EmailAddressBuilder
        {
            private string _userName = string.Empty;
            private string _emailHost = string.Empty;

            /// <summary>
            /// Sets the user part of the email address.
            /// </summary>
            /// <param name="userName">The user part of the email address.</param>
            /// <returns>The <see cref="EmailAddressBuilder"/> instance.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the user part is null or empty.</exception>
            public EmailAddressBuilder WithUserName(string userName)
            {
                if (string.IsNullOrWhiteSpace(userName))
                    throw new ArgumentNullException(nameof(userName));

                _userName = userName.Trim();
                return this;
            }

            /// <summary>
            /// Sets the host part of the email address.
            /// </summary>
            /// <param name="emailHost">The host part of the email address.</param>
            /// <returns>The <see cref="EmailAddressBuilder"/> instance.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the host part is null or empty.</exception>
            public EmailAddressBuilder OnHost(string emailHost)
            {
                if (string.IsNullOrWhiteSpace(emailHost))
                    throw new ArgumentNullException(nameof(emailHost));

                _emailHost = emailHost.Trim();
                return this;
            }

            /// <summary>
            /// Tries to build an <see cref="EmailAddress"/> from the provided email address parts.
            /// </summary>
            /// <returns>A valid <see cref="EmailAddress"/> instance.</returns>
            /// <exception cref="InvalidEmailAddressException">Thrown when the given parts lead to an invalid email.</exception>
            public EmailAddress TryBuild()
            {
                EmailStringValidator.Validate($"{_userName}@{_emailHost}");
                return new EmailAddress(_userName, _emailHost);
            }
        }
    }

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
}
