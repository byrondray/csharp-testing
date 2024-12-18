using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment3.Models
{
    public class Agency
    {
        public string Name { get; }
        private readonly Dictionary<string, Property> properties = new Dictionary<string, Property>();

        public Agency(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Agency name cannot be null or empty.");

            Name = name;
        }

        public void AddProperty(Property property)
        {
            if (property == null || properties.ContainsKey(property.PropertyId))
                throw new ArgumentException("Property is null or already exists.");

            properties[property.PropertyId] = property;
        }

        public void RemoveProperty(string propertyId)
        {
            if (string.IsNullOrEmpty(propertyId) || !properties.ContainsKey(propertyId))
                throw new ArgumentException("Invalid property ID or property not found.");

            properties.Remove(propertyId);
        }

        public Property GetProperty(string propertyId)
        {
            properties.TryGetValue(propertyId, out var property);
            return property;
        }

        public double GetTotalPropertyValues()
        {
            return properties.Values.Sum(p => p.PriceUsd);
        }

        public List<Property> GetPropertiesWithPools()
        {
            return properties.Values.Where(p => p.HasSwimmingPool).ToList();
        }

        public Property[] GetPropertiesBetween(double minPrice, double maxPrice)
        {
            return properties.Values.Where(p => p.PriceUsd >= minPrice && p.PriceUsd <= maxPrice).ToArray();
        }

        public List<Address> GetPropertiesOn(string streetName)
        {
            if (string.IsNullOrEmpty(streetName))
                return null;

            var result = properties.Values
                .Where(p => p.Address.StreetName.Equals(streetName, StringComparison.OrdinalIgnoreCase))
                .Select(p => p.Address)
                .ToList();

            return result.Any() ? result : null;
        }

        public Dictionary<string, Property> GetPropertiesWithBedrooms(int min, int max)
        {
            var result = properties.Values
                .Where(p => p.NumberOfBedrooms >= min && p.NumberOfBedrooms <= max)
                .ToDictionary(p => p.PropertyId);

            return result.Any() ? result : null;
        }

        public List<string> GetPropertiesOfType(string propertyType)
        {
            if (string.IsNullOrEmpty(propertyType))
                throw new ArgumentException("Property type cannot be null or empty.", nameof(propertyType));

            var result = properties.Values
                .Where(p => p.Type.Equals(propertyType, StringComparison.OrdinalIgnoreCase))
                .Select((p, i) => $"{i + 1}) Property {p.PropertyId}: unit #{p.Address?.UnitNumber} at {p.Address?.StreetNumber} {p.Address?.StreetName} {p.Address?.PostalCode} in {p.Address?.City} ({p.NumberOfBedrooms} bedroom{(p.NumberOfBedrooms > 1 ? "s" : "")}{(p.HasSwimmingPool ? " plus pool" : "")}): ${p.PriceUsd}.")
                .ToList();

            if (!result.Any())
                result.Add($"Type: {propertyType.ToUpperInvariant()}\n<none found>");

            return result;
        }
    }
}

