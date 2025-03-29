using System.Text.Json.Serialization;

namespace Sisusa.Information.Communication
{
    /// <summary>
    /// Represents an email address with user part and host part.
    /// </summary>
    [JsonConverter(typeof(EmailAddressConverter))]
    public class EmailAddress
    {
        /// <summary>
        /// Represents a stub email address.
        /// </summary>
        public static readonly EmailAddress None = new("stub", "domain.tld");

        /// <summary>
        /// The user part of the email address i.e. the part before @.
        /// </summary>
        public string UserName { get; init; }

        /// <summary>
        /// The email provider - part after the @.
        /// </summary>
        public string EmailHost { get; init; }

        [JsonIgnore]
        public string Email => $"{UserName}@{EmailHost}";

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailAddress"/> class.
        /// </summary>
        /// <param name="userName">The user part of the email address.</param>
        /// <param name="emailHost">The domain part of the email address.</param>
        private EmailAddress(string userName, string emailHost)
        {
            UserName = userName;
            EmailHost = emailHost;
        }

        /// <summary>
        /// Creates a new instance by parsing an existing email-string.
        /// </summary>
        /// <param name="email">The email address to wrap into an instance.</param>
        /// <exception cref="ArgumentException">If given an empty or null string.</exception>
        /// <exception cref="InvalidEmailAddressException">If given a non-email string.</exception>
        public EmailAddress(string email)
        {
            var parsed = EmailAddress.Parse(email);
            UserName = parsed.UserName;
            EmailHost = parsed.EmailHost;
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
}
