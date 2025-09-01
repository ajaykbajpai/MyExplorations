using Bookify.CA.Domain.Apartments.Entities;
using Bookify.CA.Domain.Bookings.Entities;
using Bookify.CA.Domain.Shared.Types.Values;

namespace Bookify.CA.Domain.Bookings.Repos;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Apartment apartment,
        DateRange duration,
        CancellationToken cancellationToken = default);

    void Add(Booking booking);
}