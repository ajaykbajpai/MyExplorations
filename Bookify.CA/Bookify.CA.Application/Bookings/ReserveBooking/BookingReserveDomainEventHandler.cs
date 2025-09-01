using Bookify.CA.Application.Abstractions.Email;
using Bookify.CA.Domain.Bookings.Events;
using Bookify.CA.Domain.Bookings.Repos;
using Bookify.CA.Domain.Users.Repos;
using MediatR;

namespace Bookify.CA.Application.Bookings.ReserveBooking;

public sealed class BookingReserveDomainEventHandler(
    IBookingRepository bookingRepository,
    IUserRepository userRepository,
    IEmailService emailService
    ) : INotificationHandler<BookingReservedDomainEvent>
{
    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);

        if (booking is null)
        {
            return;
        }
        
        var user = await userRepository.GetByIdAsync(booking.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }
        
        // Better to use DB queries using Dappr to get subject and body.
        await emailService.SendAsync(user.Email,
            "Booking is reserved",
            "You have 10 minutes to confirm booking.");
    }
}