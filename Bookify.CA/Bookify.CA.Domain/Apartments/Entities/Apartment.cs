using Bookify.CA.Domain.Abstractions;
using Bookify.CA.Domain.Apartments.Enums;
using Bookify.CA.Domain.Apartments.Types.Values;
using Bookify.CA.Domain.Shared.Types.Values;

namespace Bookify.CA.Domain.Apartments.Entities;

public sealed class Apartment(
    Guid id,
    Name name,
    Description description,
    Address address,
    Money price,
    Money cleaningFee,
    List<Amenity> amenities)
    : Entity(id)
{
    public Name Name { get; private set; } = name;
    public Description Description { get; private set; } = description;
    public Address Address { get; private set; } = address;
    public Money Price { get; private set; } = price;
    public Money CleaningFee { get; private set; } = cleaningFee;
    public DateTime? LastBookedOnUtc { get; internal set; }
    public List<Amenity> Amenities { get; private set; } = amenities;
}