namespace Sisusa.Information.Communication
{
    /// <summary>
    /// Represents a postal address with various components such as prefix, box number, postal code, town or city, and country.
    /// </summary>
    public class PostalAddress : IEquatable<PostalAddress>
    {
        /// <summary>
        /// Gets the prefix of the postal address.
        /// </summary>
        public string Prefix { get; }

        /// <summary>
        /// Gets the box number of the postal address.
        /// </summary>
        public int BoxNumber { get; }

        /// <summary>
        /// Gets the postal code of the postal address.
        /// </summary>
        public string PostalCode { get; }

        /// <summary>
        /// Gets the town or city of the postal address.
        /// </summary>
        public string TownOrCity { get; }

        /// <summary>
        /// Gets the country of the postal address.
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Gets or sets the recipient of the postal address.
        /// </summary>
        public string? AddressTo { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostalAddress"/> class.
        /// </summary>
        /// <param name="prefix">The prefix of the postal address.</param>
        /// <param name="boxNumber">The box number of the postal address.</param>
        /// <param name="postalCode">The postal code of the postal address.</param>
        /// <param name="townOrCity">The town or city of the postal address.</param>
        /// <param name="country">The country of the postal address.</param>
        private PostalAddress(string prefix, int boxNumber, string postalCode, string townOrCity, string country)
        {
            //kept private to enforce use of builder

            Prefix = prefix.Trim();
            BoxNumber = boxNumber;
            PostalCode = postalCode.Trim();
            TownOrCity = townOrCity.Trim();
            Country = country.Trim();
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Prefix, BoxNumber, PostalCode, TownOrCity, Country);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return GetFullAddress();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is PostalAddress pO)
            {
                return GetHashCode() == pO.GetHashCode();
            }
            return false;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(PostalAddress? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Prefix == other.Prefix &&
                   BoxNumber == other.BoxNumber &&
                   PostalCode == other.PostalCode &&
                   TownOrCity == other.TownOrCity &&
                   Country == other.Country &&
                   AddressTo == other.AddressTo;
        }

        /// <summary>
        /// Gets the full address as a formatted string.
        /// </summary>
        /// <returns>The full address as a string.</returns>
        public string GetFullAddress()
        {
            if (string.IsNullOrWhiteSpace(AddressTo))
            {
                return $"{Prefix} {BoxNumber}, {PostalCode} {TownOrCity}, {Country}";
            }
            return $"{AddressTo}\n{Prefix} {BoxNumber}, {PostalCode} {TownOrCity}, {Country}";
        }

        public static bool operator ==(PostalAddress? a, PostalAddress b)
        {
            if (a is null || ReferenceEquals(a, null))
                return b is null || ReferenceEquals(b, null);
            return ReferenceEquals(a, b) || a.Equals(b);
        }

        public static bool operator !=(PostalAddress a, PostalAddress b)
        {
            return !(a == b);
        }

        public static PostalAddressBuilder WithPrefix(string prefix)
        {
            return new PostalAddressBuilder()
                .WithPrefix(prefix);
        }

        /// <summary>
        /// Builder class for constructing <see cref="PostalAddress"/> instances.
        /// </summary>
        public class PostalAddressBuilder
        {
            private string _prefix = string.Empty;
            private int _boxNumber;
            private string _postalCode = string.Empty;
            private string _townOrCity = string.Empty;
            private string _country = string.Empty;
            private string _addressTo = string.Empty;

            /// <summary>
            /// Sets the prefix for the postal address.
            /// </summary>
            /// <param name="prefix">The prefix to set.</param>
            /// <returns>The current instance of <see cref="PostalAddressBuilder"/>.</returns>
            public PostalAddressBuilder WithPrefix(string prefix)
            {
                _prefix = prefix;
                return this;
            }

            /// <summary>
            /// Sets the box number for the postal address.
            /// </summary>
            /// <param name="boxNumber">The box number to set.</param>
            /// <returns>The current instance of <see cref="PostalAddressBuilder"/>.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the box number is less than or equal to zero.</exception>
            public PostalAddressBuilder WithBoxNumber(int boxNumber)
            {
                if (boxNumber <= 0)
                {
                    throw new ArgumentNullException(nameof(boxNumber), "Valid box or bag number expected.");
                }
                _boxNumber = boxNumber;
                return this;
            }

            /// <summary>
            /// Sets the postal code for the postal address.
            /// </summary>
            /// <param name="postalCode">The postal code to set.</param>
            /// <returns>The current instance of <see cref="PostalAddressBuilder"/>.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the postal code is null or whitespace.</exception>
            public PostalAddressBuilder WithPostalCode(string postalCode)
            {
                if (string.IsNullOrWhiteSpace(postalCode))
                {
                    throw new ArgumentNullException(nameof(postalCode), "Valid Postal code should be given.");
                }
                _postalCode = postalCode;
                return this;
            }

            /// <summary>
            /// Sets the town or city for the postal address.
            /// </summary>
            /// <param name="townOrCity">The town or city to set.</param>
            /// <returns>The current instance of <see cref="PostalAddressBuilder"/>.</returns>
            public PostalAddressBuilder WithTownOrCity(string townOrCity)
            {
                _townOrCity = townOrCity;
                return this;
            }

            /// <summary>
            /// Sets the country for the postal address.
            /// </summary>
            /// <param name="country">The country to set.</param>
            /// <returns>The current instance of <see cref="PostalAddressBuilder"/>.</returns>
            public PostalAddressBuilder WithCountry(string country)
            {
                _country = country;
                return this;
            }

            /// <summary>
            /// Sets the recipient for the postal address.
            /// </summary>
            /// <param name="addressTo">The recipient to set.</param>
            /// <returns>The current instance of <see cref="PostalAddressBuilder"/>.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the recipient is null or whitespace.</exception>
            public PostalAddressBuilder AddressedTo(string addressTo)
            {
                if (string.IsNullOrWhiteSpace(addressTo))
                {
                    throw new ArgumentNullException(nameof(addressTo), "Since this has been called, Addressed to should be provided.");
                }
                _addressTo = addressTo;
                return this;
            }

            /// <summary>
            /// Builds the <see cref="PostalAddress"/> instance.
            /// </summary>
            /// <returns>A new instance of <see cref="PostalAddress"/>.</returns>
            /// <exception cref="ArgumentNullException">Thrown when required fields are not provided.</exception>
            public PostalAddress Build()
            {
                if (string.IsNullOrWhiteSpace(_country))
                {
                    throw new ArgumentNullException(nameof(_country), "Country name should be provided.");
                }
                if (string.IsNullOrWhiteSpace(_townOrCity))
                {
                    throw new ArgumentNullException(nameof(_townOrCity), "Town or city name should be provided.");
                }
                if (_boxNumber <= 0)
                {
                    throw new ArgumentNullException(nameof(_boxNumber), "Valid box or bag number expected.");
                }
                if (string.IsNullOrWhiteSpace(_postalCode))
                {
                    throw new ArgumentNullException(nameof(_postalCode), "Postal code should be given.");
                }
                if (string.IsNullOrWhiteSpace(_prefix))
                {
                    _prefix = "P.O.";
                }
                var poAddress = new PostalAddress(_prefix, _boxNumber, _postalCode, _townOrCity, _country);
                if (string.IsNullOrWhiteSpace(_addressTo))
                {
                    return poAddress;
                }
                poAddress.AddressTo = _addressTo.Trim();
                return poAddress;
            }
        }
    }
}
