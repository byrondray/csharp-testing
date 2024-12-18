public class Address
{
    public string UnitNumber { get; }
    public int StreetNumber { get; }
    public string StreetName { get; }
    public string PostalCode { get; }
    public string City { get; }

    public Address(string unitNumber, int streetNumber, string streetName, string postalCode, string city)
    {
        // Validate Unit Number
        if (!string.IsNullOrEmpty(unitNumber) && unitNumber.Length > 5)
            throw new ArgumentException($"Invalid unit number: {unitNumber}");
        if (unitNumber == "")
            throw new ArgumentException("Invalid unit number: ");

        // Validate Street Number
        if (streetNumber <= 0 || streetNumber > 99999)
            throw new ArgumentException($"Invalid street number: {streetNumber}");

        // Validate Street Name
        if (streetName == null)
            throw new ArgumentNullException(nameof(streetName), "Invalid street name: null");
        if (streetName == "")
            throw new ArgumentException("Invalid street name: ");
        if (streetName.Length > 20)
            throw new ArgumentException($"Invalid street name: {streetName}");

        // Validate Postal Code
        if (postalCode == null)
            throw new ArgumentNullException(nameof(postalCode), "Invalid postal code: null");
        if (string.IsNullOrEmpty(postalCode) || postalCode.Length < 5 || postalCode.Length > 6)
            throw new ArgumentException($"Invalid postal code: {postalCode}");

        // Validate City
        if (city == null)
            throw new ArgumentNullException(nameof(city), "Invalid city: null");
        if (city == "")
            throw new ArgumentException("Invalid city: ");
        if (city.Length > 30)
            throw new ArgumentException($"Invalid city: {city}");

        // Assign Fields
        UnitNumber = unitNumber;
        StreetNumber = streetNumber;
        StreetName = streetName;
        PostalCode = postalCode;
        City = city;
    }
}
