using Bookify.CA.Domain.Abstractions;
using Bookify.CA.Domain.Apartments.Enums;
using Bookify.CA.Domain.Apartments.Types.Values;
using Bookify.CA.Domain.Shared.Types.Values;

namespace Bookify.CA.Domain.Apartments.Entities;

public sealed class Apartment : Entity
{
    private Apartment(
        Guid id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        List<Amenity> amenities) : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        Amenities = amenities;
    }

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Address Address { get; private set; }
    public Money Price { get; private set; }
    public Money CleaningFee { get; private set; }
    // Setter is made internal so that it could be set within the domain project.
    // It will be set during reserve booking workflow.  
    public DateTime? LastBookedOnUtc { get; internal set; }
    public List<Amenity> Amenities { get; private set; }
    
    /// <summary>
    /// Create static factory method. This must be the only way to create an apartment.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="address"></param>
    /// <param name="price"></param>
    /// <param name="cleaningFee"></param>
    /// <param name="amenities"></param>
    /// <returns>Apartment Result</returns>
    public static Result<Apartment> Create(
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        Amenity[] amenities)
    {
        Apartment apartment = new(
            Guid.NewGuid(),
            name,
            description,
            address,
            price,
            cleaningFee,
            amenities.ToList());

        return Result.Success(apartment);
    }

    public void Update(
        Money priceAmount,
        Money cleaningFeeAmount,
        Amenity[] amenities)
    {
        Price = priceAmount;
        CleaningFee = cleaningFeeAmount;
        Amenities = amenities.ToList();
    }
}