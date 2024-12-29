using Bookify.CA.Domain.Abstractions;

namespace Bookify.CA.Domain.Bookings.Events;

public sealed record BookingCancelledDomainEvent(Guid BookingId) : IDomainEvent;