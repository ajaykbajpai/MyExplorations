using Bookify.CA.Domain.Shared.Types.Values;

namespace Bookify.CA.Domain.Bookings.Types.Values;

public record PricingDetails(
    Money PriceForPeriod,
    Money CleaningFee,
    Money AmenitiesUpCharge,
    Money TotalPrice);