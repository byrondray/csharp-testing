using System;

namespace Assignment3.Models
{
    public class Property
    {
        public double PriceUsd { get; set; }
        public Address Address { get; }
        public int NumberOfBedrooms { get; }
        public bool HasSwimmingPool { get; }
        public string Type { get; }
        public string PropertyId { get; }

        public Property(double priceUsd, Address address, int numberOfBedrooms, bool hasSwimmingPool, string type, string propertyId)
        {
            // Validate Price
            if (priceUsd <= 0)
                throw new ArgumentException($"Invalid price: {priceUsd}");

            // Validate Address
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Invalid address: null");

            // Validate Number of Bedrooms
            if (numberOfBedrooms < 1 || numberOfBedrooms > 20)
                throw new ArgumentException($"Invalid number of bedrooms: {numberOfBedrooms}");

            // Validate Type
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type), "Invalid property type: null");

            if (!IsValidType(type))
                throw new ArgumentException($"Invalid property type: {type}");

            // Validate Property ID
            if (propertyId == null)
                throw new ArgumentNullException(nameof(propertyId), "Invalid property id: null");

            if (string.IsNullOrEmpty(propertyId))
                throw new ArgumentException("Invalid property id: ");

            if (propertyId.Length > 6)
                throw new ArgumentException($"Invalid property id: {propertyId}");

            // Assign Properties
            PriceUsd = priceUsd;
            Address = address;
            NumberOfBedrooms = numberOfBedrooms;
            HasSwimmingPool = hasSwimmingPool;
            Type = type; // Retain original casing
            PropertyId = propertyId;
        }

        private bool IsValidType(string type)
        {
            var validTypes = new[] { "residence", "commercial", "retail" };
            return Array.Exists(validTypes, t => t.Equals(type, StringComparison.OrdinalIgnoreCase));
        }
    }
}
