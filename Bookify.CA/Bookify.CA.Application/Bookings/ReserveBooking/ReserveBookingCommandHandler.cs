using Bookify.CA.Application.Abstractions.Clock;
using Bookify.CA.Application.Abstractions.Messaging;
using Bookify.CA.Domain.Abstractions;
using Bookify.CA.Domain.Apartments.Repos;
using Bookify.CA.Domain.Apartments.Responses;
using Bookify.CA.Domain.Bookings.Entities;
using Bookify.CA.Domain.Bookings.Repos;
using Bookify.CA.Domain.Bookings.Responses;
using Bookify.CA.Domain.Bookings.Services;
using Bookify.CA.Domain.Shared.Types.Values;
using Bookify.CA.Domain.Users.Repos;
using Bookify.CA.Domain.Users.Responses;

namespace Bookify.CA.Application.Bookings.ReserveBooking;

// The handler will never be exposed directly outside application layer hence internal.
internal sealed class ReserveBookingCommandHandler(
    IUserRepository userRepository,
    IApartmentRepository apartmentRepository,
    IBookingRepository bookingRepository,
    IUnitOfWork unitOfWork,
    PricingService pricingService,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<ReserveBookingCommand, Guid>
{
    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null) return Result.Failure<Guid>(UserErrors.NotFound);

        var apartment = await apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);
        if (apartment is null) return Result.Failure<Guid>(ApartmentErrors.NotFound);

        var duration = DateRange.Create(request.StartDate, request.EndDate);
        if (await bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
            return Result.Failure<Guid>(BookingErrors.Overlap);

        var booking = Booking.Reserve(apartment,
            request.UserId,
            duration,
            dateTimeProvider.UtcNow,
            pricingService.CalculatePrice(apartment, duration));
        
        bookingRepository.Add(booking.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(booking.Value.Id);
    }
}