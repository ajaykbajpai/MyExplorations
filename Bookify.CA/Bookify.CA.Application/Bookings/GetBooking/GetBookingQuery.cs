using Bookify.CA.Application.Abstractions.Messaging;
using Bookify.CA.Application.Bookings.GetBooking.Types;

namespace Bookify.CA.Application.Bookings.GetBooking;

public sealed record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>;