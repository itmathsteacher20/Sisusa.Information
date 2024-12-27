using System.Collections.ObjectModel;

namespace Sisusa.Information.Identification
{
    /// <summary>
    /// Represents a person's name, including first name, middle names, and last name.
    /// </summary>
    public class NameOfPerson
    {
        /// <summary>
        /// Gets the first name of the person.
        /// </summary>
        public string Firstname { get; private set; }

        /// <summary>
        /// Gets the middle names of the person.
        /// </summary>
        public string MiddleNames { get; private set; }

        /// <summary>
        /// Gets the last name of the person.
        /// </summary>
        public string Lastname { get; private set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>True if the objects are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            if (obj is NameOfPerson pName)
            {
                return string.Equals(Firstname, pName.Firstname, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(Lastname, pName.Lastname, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(MiddleNames, pName.MiddleNames, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        /// <summary>
        /// Gets the hash code for the current instance.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Firstname, MiddleNames, Lastname);
        }

        /// <summary>
        /// Returns the full name of the person as a string.
        /// </summary>
        /// <returns>The full name of the person.</returns>
        public override string ToString()
        {
            return GetFullname();
        }

        /// <summary>
        /// Checks if two <see cref="NameOfPerson"/> instances are equal.
        /// </summary>
        /// <param name="first">The first <see cref="NameOfPerson"/> instance.</param>
        /// <param name="second">The second <see cref="NameOfPerson"/> instance.</param>
        /// <returns>True if the instances are equal; otherwise, false.</returns>
        public static bool operator ==(NameOfPerson? first, NameOfPerson second)
        {
            if (ReferenceEquals(first, null) || first is null) 
                return ReferenceEquals(second, null) || second is null;
            
            return ReferenceEquals(first, second) || first.Equals(second);
        }

        /// <summary>
        /// Checks if two <see cref="NameOfPerson"/> instances are not equal.
        /// </summary>
        /// <param name="first">The first <see cref="NameOfPerson"/> instance.</param>
        /// <param name="second">The second <see cref="NameOfPerson"/> instance.</param>
        /// <returns>True if the instances are not equal; otherwise, false.</returns>
        public static bool operator !=(NameOfPerson? first, NameOfPerson second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameOfPerson"/> class.
        /// </summary>
        /// <param name="lastName">The last name of the person.</param>
        /// <param name="firstName">The first name of the person.</param>
        /// <param name="otherNames">Optional. The middle names of the person.</param>
        private NameOfPerson(string lastName, string firstName, string otherNames = "")
        {
            Lastname = lastName;
            Firstname = firstName;
            MiddleNames = otherNames;
        }

        /// <summary>
        /// Indicates whether the person has middle names.
        /// </summary>
        public bool HasOtherNames => !string.IsNullOrWhiteSpace(MiddleNames);

        /// <summary>
        /// Gets the full name of the person in the specified order.
        /// </summary>
        /// <param name="nameOrder">The order of the names.</param>
        /// <returns>The full name of the person.</returns>
        public string GetFullname(NameOrder nameOrder = NameOrder.SurnameNames)
        {
            ValidateOrThrow("Could not get full name because instance is not valid.");

            if (nameOrder != NameOrder.SurnameNames)
            {
                return HasOtherNames ? $"{Firstname} {MiddleNames} {Lastname}" :
                    $"{Firstname} {Lastname}";
            }

            var names = $"{Lastname} {Firstname}";
            return HasOtherNames ? $"{names} {MiddleNames}" : names;
        }

        /// <summary>
        /// Gets the initials of the person's name in the specified order.
        /// </summary>
        /// <param name="nameOrder">The order of the names.</param>
        /// <returns>The initials of the person's name.</returns>
        public string GetInitials(NameOrder nameOrder = NameOrder.SurnameNames)
        {
            ValidateOrThrow("Could not get name initials because instance is not valid.");

            var nameBits = this.GetNameInitials();

            List<string> bitsList = nameOrder == NameOrder.SurnameNames
                ? [nameBits.lastNameInitial.ToString(), nameBits.firstNameInitial.ToString(), nameBits.otherNamesInitials]
                : [nameBits.firstNameInitial.ToString(), nameBits.otherNamesInitials, nameBits.lastNameInitial.ToString()];
            return $"{string.Join('.', bitsList)}.";
        }

        /// <summary>
        /// Gets the shortened name of the person in the specified order.
        /// </summary>
        /// <param name="nameOrder">The order of the names.</param>
        /// <returns>The shortened name of the person.</returns>
        public string GetShortenedName(NameOrder nameOrder = NameOrder.SurnameNames)
        {
            ValidateOrThrow("Could not get shortened name because instance is not valid.");
            var nameBits = this.GetNameInitials();
            var theInitials = string.Join(".", [nameBits.firstNameInitial, nameBits.otherNamesInitials]);
            return nameOrder == NameOrder.SurnameNames ? $"{Lastname} {theInitials}.":
                    $"{theInitials}. {Lastname}";
        }

        /// <summary>
        /// Creates a new instance of <see cref="NameOfPersonBuilder"/> for building a <see cref="NameOfPerson"/>.
        /// </summary>
        /// <returns>A new <see cref="NameOfPersonBuilder"/> instance.</returns>
        public static NameOfPersonBuilder New() => new NameOfPersonBuilder();

        /// <summary>
        /// Validates the current instance and throws an exception if invalid.
        /// </summary>
        /// <param name="errorMsg">The error message to include in the exception.</param>
        private void ValidateOrThrow(string errorMsg = "Invalid name - name should have at least valid first and last names.")
        {
            if (NameOfPersonValidator.Validate(this).Count > 0)
            {
                throw new InvalidNameOfPersonException(errorMsg);
            }
        }

        /// <summary>
        /// A builder class for creating instances of <see cref="NameOfPerson"/>.
        /// </summary>
        public class NameOfPersonBuilder
        {
            private string _firstName = string.Empty;
            private string _lastName = string.Empty;
            private string _middleName = string.Empty;

            /// <summary>
            /// Specifies the first name of the person.
            /// </summary>
            /// <param name="firstName">The first name.</param>
            /// <returns>The current <see cref="NameOfPersonBuilder"/> instance.</returns>
            public NameOfPersonBuilder WithFirstName(string firstName)
            {
                this._firstName = firstName.Trim();
                return this;
            }

            /// <summary>
            /// Specifies the middle names of the person.
            /// </summary>
            /// <param name="otherNames">The middle names.</param>
            /// <returns>The current <see cref="NameOfPersonBuilder"/> instance.</returns>
            public NameOfPersonBuilder HavingOtherNames(string otherNames)
            {
                _middleName = otherNames.Trim();
                return this;
            }

            /// <summary>
            /// Specifies the last name of the person.
            /// </summary>
            /// <param name="lastName">The last name.</param>
            /// <returns>The current <see cref="NameOfPersonBuilder"/> instance.</returns>
            public NameOfPersonBuilder AndLastName(string lastName)
            {
                this._lastName = lastName.Trim();
                return this;
            }

            /// <summary>
            /// Attempts to build a <see cref="NameOfPerson"/> instance.
            /// </summary>
            /// <returns>A new <see cref="NameOfPerson"/> instance.</returns>
            /// <exception cref="InvalidNameOfPersonException">Thrown if the first or last name is invalid.</exception>
            public NameOfPerson TryBuild()
            {
                if (string.IsNullOrWhiteSpace(_firstName) || string.IsNullOrWhiteSpace(_lastName))
                {
                    throw new InvalidNameOfPersonException();
                }
                return new(lastName: _lastName, firstName: _firstName, otherNames: _middleName);
            }
        }
    }

    /// <summary>
    /// Defines the order of a person's name.
    /// </summary>
    public enum NameOrder : ushort
    {
        /// <summary>
        /// Surname followed by other names.
        /// </summary>
        SurnameNames = 0,

        /// <summary>
        /// Other names followed by surname.
        /// </summary>
        NamesSurname = 1
    }

    /// <summary>
    /// Exception thrown when a person's name is invalid.
    /// </summary>
    public sealed class InvalidNameOfPersonException(
        string msg = "Name of person must at least have a valid firstname and surname.")
        : Exception(msg);

    /// <summary>
    /// Provides validation logic for <see cref="NameOfPerson"/> instances.
    /// </summary>
    public static class NameOfPersonValidator
    {
        /// <summary>
        /// Validates the specified <see cref="NameOfPerson"/> instance.
        /// </summary>
        /// <param name="person">The <see cref="NameOfPerson"/> instance to validate.</param>
        /// <returns>A collection of validation error messages.</returns>
        public static ReadOnlyCollection<string> Validate(NameOfPerson person)
        {
            if (ReferenceEquals(null, person))
                return new ReadOnlyCollection<string>(["Name should be a non-null value."]);

            var failures = new List<string>();
            if (string.IsNullOrWhiteSpace(person.Firstname))
            {
                failures.Add("Firstname should not be empty.");
            }

            if (string.IsNullOrWhiteSpace(person.Lastname))
            {
                failures.Add("Lastname should not be empty.");
            }
            return new ReadOnlyCollection<string>(failures);
        }
    }

    internal static class NameOfPersonExtensions
    {
        /// <summary>
        /// Gets the initials of a <see cref="NameOfPerson"/>.
        /// </summary>
        /// <param name="person">The <see cref="NameOfPerson"/> instance.</param>
        /// <returns>A tuple containing the initials of the first name, last name, and middle names.</returns>
        internal static (char firstNameInitial, char lastNameInitial, string otherNamesInitials) GetNameInitials(
            this NameOfPerson person)
        {
            if (!person.HasOtherNames)
            {
                return (person.Firstname.First(),person.Lastname.First(), string.Empty);
            }

            var otherNames = person.MiddleNames.Split(' ');

            List<char> foreNameInitials = [];
            foreach (var oName in otherNames)
            {
                foreNameInitials.Add(oName.First());
            }
            var foreName = string.Join('.', foreNameInitials);
            return (person.Firstname.First(), person.Lastname.First(), foreName);
        }
    }
}
