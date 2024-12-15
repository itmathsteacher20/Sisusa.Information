using System.Text.RegularExpressions;

namespace Sisusa.Information.Communication
{
    public class EmailAddress
    {
        public static readonly EmailAddress None = new("stub", "domain.tld");
        /// <summary>
        /// The user part of the email address i.e. the part before @.
        /// </summary>
        public string UserName {get; private set;}

        /// <summary>
        /// The email provider - part after the @
        /// </summary>
        public string EmailHost {get; private set;}

        public override int GetHashCode()
        {
            return HashCode.Combine(UserName, EmailHost);
        }

        public override string ToString()
        {
            return $"{UserName}@{EmailHost}";
        }

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

        public static bool operator ==(EmailAddress a, EmailAddress b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null) return false;
            if (b is null) return false;
           // if (a == null || b == null) return false;
            
            return a.Equals(b);
        }

        public static bool operator !=(EmailAddress a, EmailAddress b)
        {
            return !(a == b);
        }

        public static implicit operator string(EmailAddress emailAddress)
        {
            return emailAddress is null ? string.Empty : emailAddress.ToString();
        }

        //private static Regex EmailFormat = new("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");
        private EmailAddress(string userPart, string domainName)
        {
            UserName = userPart;
            EmailHost = domainName;
        }
        
        public static EmailAddressBuilder BuildAddress => new();

        public static EmailAddress Parse(string emailAddress)
        {
            EmailStringValidator.Validate(emailAddress);
            return EmailAddressParser.Parse(emailAddress);
        }

        public class EmailAddressBuilder
        {
            private string _userName = string.Empty;
            private string _emailHost = string.Empty;

            public EmailAddressBuilder WithUserName(string userName)
            {
                if (string.IsNullOrWhiteSpace(userName)) 
                    throw new ArgumentNullException(nameof(userName));
                
                _userName = userName.Trim();
                return this;
            }

            public EmailAddressBuilder OnHost(string emailHost)
            {
                if (string.IsNullOrWhiteSpace(emailHost)) 
                    throw new ArgumentNullException(nameof(emailHost));
                
                _emailHost = emailHost.Trim();
                return this;
            }

            /// <summary>
            /// Tries to build an EmailAddress from the provided email address parts.
            /// </summary>
            /// <returns>A valid EmailAddress instance</returns>
            /// <exception cref="InvalidEmailAddressException">If the given parts lead to an invalid email.</exception>
            public EmailAddress TryBuild()
            {
                EmailStringValidator.Validate($"{_userName}@{_emailHost}");
                return new EmailAddress(_userName, _emailHost);
            }
        }
        
    }

    public static class EmailStringValidator
    {
        private static readonly Regex EmailFormat = new("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$");

        static void ThrowIfNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidEmailAddressException(message);
            }
        }

        static void ThrowIfContainsInvalidCharacters(string value, string message)
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
        
        public static void Validate(string emailAddress)
        {
            ThrowIfNullOrEmpty(emailAddress, "Email address to validate cannot be empty.");
            ThrowIfContainsInvalidCharacters(emailAddress, "Given string is not valid email address(for this application).");
            
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
    }

    public static class EmailAddressParser
    {
        static (string localPart, string domainPart) SplitToParts(string emailAddress)
        {
            var emailParts = emailAddress.Split('@');
            return (emailParts[0], emailParts[1]);
        }
        public static EmailAddress Parse(string emailAddress)
        {
            EmailStringValidator.Validate(emailAddress);
            var emailParts = SplitToParts(emailAddress);
            return EmailAddress.BuildAddress
                .WithUserName(emailParts.localPart)
                .OnHost(emailParts.domainPart)
                .TryBuild();
        }
    }
    public sealed class InvalidEmailAddressException(string message = "Provided string does not appear to be a valid email address."):Exception(message);
    public sealed class InvalidEmailProviderFormatException():Exception("Specified email provider(or hosting service) looks invalid.");
    public sealed class InvalidEmailUsernameException(): Exception("Invalid email format - user part contains invalid characters.");
}
