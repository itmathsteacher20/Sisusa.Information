namespace Sisusa.Information.Communication
{
    public class DetailedAddress
    {
        /// <summary>
        /// Name of country in which address/location is located
        /// </summary>
        public string Country { get; private set;}

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
        public string NearestGeographicalMarker { get; private set;}
        
        /// <summary>
        /// The Zip Code of this address
        /// e.g. H100, S401, etc.
        /// </summary>
        public string PostalCode { get; private set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            if (obj is DetailedAddress addressDetails)
            {
                return GetHashCode() == addressDetails.GetHashCode();
            }

            return false;
        }

        public bool Equals(DetailedAddress? other)
        {
            if (other == null) return false;
            return Equals(Country, other.Country);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Country, City, StreetName, NearestGeographicalMarker, PostalCode);
        }

        public static AddressBuilder GetBuilder() => new AddressBuilder();

        private DetailedAddress(string townName, string nearestGeographicalMarker, string countryName = "Eswatini")
        {
            City = townName;
            StreetName = string.Empty;
            PostalCode = string.Empty;
            NearestGeographicalMarker = nearestGeographicalMarker;
            Country = countryName;
        }

        public class AddressBuilder
        {
            private string _town = string.Empty;
            private string _street = string.Empty;
            private string _postalCode = string.Empty;
            private string _geographicalMark = string.Empty;
            private string _country = "Eswatini";

            public AddressBuilder FromCountry(string countryName="Eswatini")
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

            public AddressBuilder HavingZipCode(string zipCode)
            {
                if (string.IsNullOrWhiteSpace(zipCode))
                {
                    throw new ArgumentNullException(
                        paramName:nameof(zipCode),
                        message:"Zip Code(if provided) should be a valid value.");
                }
                _postalCode = zipCode.Trim();
                return this;
            }

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

            public AddressBuilder InStreetOrHouse(string streetOrHouse)
            {
                if (string.IsNullOrWhiteSpace(streetOrHouse))
                {
                    throw new ArgumentNullException(
                        paramName:nameof(streetOrHouse),
                        message:"Street or house name(if provided) should be a valid value.");
                }
                _street = streetOrHouse;
                return this;
            }

            public DetailedAddress Create()
            {
                var details = new DetailedAddress(_town, _geographicalMark, _country);
                details.PostalCode = _postalCode;
                details.StreetName = _street;

                return details;
            }

            public AddressBuilder() { }


        }
    }

    public enum PostalCode:ushort
    {

    }
}
