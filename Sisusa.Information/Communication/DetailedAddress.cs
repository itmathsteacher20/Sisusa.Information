namespace Sisusa.Information.Communication
{
    public class DetailedAddress
    {
        /// <summary>
        /// Name of country in which address/location is located
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// Name of town (or city) in which address is located
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Name of street, house number or even village name in which address/location is located.
        /// </summary>
        public string StreetName { get; private set; }

        /// <summary>
        /// A well-known geographical marker that is close to the address/location.
        /// Used to verify that one is indeed headed for correct location.
        /// </summary>
        public string NearestGeographicalMarker { get; private set; }

        /// <summary>
        /// The Zip Code of this address
        /// e.g. H100, S401, etc.
        /// </summary>
        public string PostalCode { get; private set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            if (obj is DetailedAddress addressDetails)
            {
                return GetHashCode() == addressDetails.GetHashCode();
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified DetailedAddress is equal to the current DetailedAddress.
        /// </summary>
        /// <param name="other">The DetailedAddress to compare with the current DetailedAddress.</param>
        /// <returns>true if the specified DetailedAddress is equal to the current DetailedAddress; otherwise, false.</returns>
        public bool Equals(DetailedAddress? other)
        {
            if (other == null) return false;
            return Equals(Country, other.Country);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Country, City, StreetName, NearestGeographicalMarker, PostalCode);
        }

        /// <summary>
        /// Gets an instance of the AddressBuilder class.
        /// </summary>
        /// <returns>An instance of the AddressBuilder class.</returns>
        public static AddressBuilder GetBuilder() => new AddressBuilder();

        /// <summary>
        /// Initializes a new instance of the DetailedAddress class.
        /// </summary>
        /// <param name="townName">The name of the town or city.</param>
        /// <param name="nearestGeographicalMarker">The nearest geographical marker.</param>
        /// <param name="countryName">The name of the country. Default is "Eswatini".</param>
        private DetailedAddress(string townName, string nearestGeographicalMarker, string countryName = "Eswatini")
        {
            City = townName;
            StreetName = string.Empty;
            PostalCode = string.Empty;
            NearestGeographicalMarker = nearestGeographicalMarker;
            Country = countryName;
        }

        /// <summary>
        /// Builder class for creating instances of DetailedAddress.
        /// </summary>
        public class AddressBuilder
        {
            private string _town = string.Empty;
            private string _street = string.Empty;
            private string _postalCode = string.Empty;
            private string _geographicalMark = string.Empty;
            private string _country = "Eswatini";

            /// <summary>
            /// Sets the country name.
            /// </summary>
            /// <param name="countryName">The name of the country. Default is "Eswatini".</param>
            /// <returns>The current instance of AddressBuilder.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the country name is null or whitespace.</exception>
            public AddressBuilder FromCountry(string countryName = "Eswatini")
            {
                if (string.IsNullOrWhiteSpace(countryName))
                {
                    throw new ArgumentNullException(
                        paramName: nameof(countryName),
                        message: "Country name should be provided.");
                }
                _country = countryName;
                return this;
            }

            /// <summary>
            /// Sets the town or city name.
            /// </summary>
            /// <param name="townOrCity">The name of the town or city.</param>
            /// <returns>The current instance of AddressBuilder.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the town or city name is null or whitespace.</exception>
            public AddressBuilder InTownOrCity(string townOrCity)
            {
                if (string.IsNullOrWhiteSpace(townOrCity))
                {
                    throw new ArgumentNullException(
                        paramName: nameof(townOrCity),
                        message: "Town or city name should be provided.");
                }
                _town = townOrCity.Trim();
                return this;
            }

            /// <summary>
            /// Sets the zip code.
            /// </summary>
            /// <param name="zipCode">The zip code.</param>
            /// <returns>The current instance of AddressBuilder.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the zip code is null or whitespace.</exception>
            public AddressBuilder HavingZipCode(string zipCode)
            {
                if (string.IsNullOrWhiteSpace(zipCode))
                {
                    throw new ArgumentNullException(
                        paramName: nameof(zipCode),
                        message: "Zip Code(if provided) should be a valid value.");
                }
                _postalCode = zipCode.Trim();
                return this;
            }

            /// <summary>
            /// Sets the nearest geographical marker.
            /// </summary>
            /// <param name="markerDescription">The description of the nearest geographical marker.</param>
            /// <returns>The current instance of AddressBuilder.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the marker description is null or whitespace.</exception>
            public AddressBuilder WithNearestGeographicalMarker(string markerDescription)
            {
                if (string.IsNullOrWhiteSpace(markerDescription))
                {
                    throw new ArgumentNullException(paramName: nameof(markerDescription),
                                                    message: "Nearest marker should be provided and valid.");
                }
                _geographicalMark = markerDescription;
                return this;
            }

            /// <summary>
            /// Sets the street or house name.
            /// </summary>
            /// <param name="streetOrHouse">The name of the street or house.</param>
            /// <returns>The current instance of AddressBuilder.</returns>
            /// <exception cref="ArgumentNullException">Thrown when the street or house name is null or whitespace.</exception>
            public AddressBuilder InStreetOrHouse(string streetOrHouse)
            {
                if (string.IsNullOrWhiteSpace(streetOrHouse))
                {
                    throw new ArgumentNullException(
                        paramName: nameof(streetOrHouse),
                        message: "Street or house name(if provided) should be a valid value.");
                }
                _street = streetOrHouse;
                return this;
            }

            /// <summary>
            /// Creates an instance of DetailedAddress with the specified properties.
            /// </summary>
            /// <returns>An instance of DetailedAddress.</returns>
            public DetailedAddress Create()
            {
                var details = new DetailedAddress(_town, _geographicalMark, _country);
                details.PostalCode = _postalCode;
                details.StreetName = _street;

                return details;
            }

            /// <summary>
            /// Initializes a new instance of the AddressBuilder class.
            /// </summary>
            public AddressBuilder() { }
        }
    }

    public enum PostalCode:ushort
    {

    }
}
