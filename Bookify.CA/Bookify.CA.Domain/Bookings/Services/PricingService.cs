using Bookify.CA.Domain.Apartments.Entities;
using Bookify.CA.Domain.Apartments.Enums;
using Bookify.CA.Domain.Bookings.Types.Values;
using Bookify.CA.Domain.Shared.Types.Values;

namespace Bookify.CA.Domain.Bookings.Services;

public class PricingService
{
    public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
    {
        var currency = apartment.Price.Currency;
        Money priceForPeriod = new(
            apartment.Price.Amount * period.LengthInDays,
            currency);

        var percentageUpCharge = CalculatePercentageUpCharge(apartment);
        var amenitiesUpCharge = CalculateAmenitiesUpCharge(percentageUpCharge, priceForPeriod, currency);
        var totalPrice = CalculateTotalPrice(apartment, priceForPeriod, amenitiesUpCharge);

        return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenitiesUpCharge, totalPrice);
    }

    private decimal CalculatePercentageUpCharge(Apartment apartment)
    {
        var percentageUpCharge = 0m;
        foreach (var amenity in apartment.Amenities)
        {
            percentageUpCharge += amenity switch
            {
                Amenity.MountainView or Amenity.LakeView => 0.05m,
                Amenity.GardenView => 0.03m,
                Amenity.AirConditioning => 0.01m,
                Amenity.Parking => 0.01m,
                _ => 0m
            };
        }

        return percentageUpCharge;
    }

    private Money CalculateAmenitiesUpCharge(decimal percentageUpCharge, Money priceForPeriod, Currency currency)
    {
        var amenitiesUpCharge = Money.Zero();
        if (percentageUpCharge > 0)
        {
            amenitiesUpCharge = new Money(priceForPeriod.Amount * percentageUpCharge, currency);
        }

        return amenitiesUpCharge;
    }

    private Money CalculateTotalPrice(Apartment apartment, Money priceForPeriod, Money amenitiesUpCharge)
    {
        var totalPrice = Money.Zero();
        totalPrice += priceForPeriod;
        if (apartment.CleaningFee.IsZero()) totalPrice += apartment.CleaningFee;
        totalPrice += amenitiesUpCharge;
        return totalPrice;
    }
}