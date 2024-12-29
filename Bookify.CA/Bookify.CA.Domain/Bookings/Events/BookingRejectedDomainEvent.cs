using Bookify.CA.Domain.Abstractions;

namespace Bookify.CA.Domain.Bookings.Events;

public sealed record BookingRejectedDomainEvent(Guid BookingId) : IDomainEvent;