using Bookify.CA.Domain.Abstractions;
using Bookify.CA.Domain.Bookings.Entities;
using Bookify.CA.Domain.Bookings.Types.Values;
using Bookify.CA.Domain.Reviews.Events;
using Bookify.CA.Domain.Reviews.Responses;
using Bookify.CA.Domain.Reviews.Types.Values;

namespace Bookify.CA.Domain.Reviews.Entities;

public sealed class Review(
    Guid id,
    Guid apartmentId,
    Guid bookingId,
    Guid userId,
    Rating rating,
    Comment comment,
    DateTime createdOnUtc) : Entity(id)
{
    public Guid ApartmentId { get; private set; } = apartmentId;
    public Guid BookingId { get; private set; } = bookingId;
    public Guid UserId { get; private set; } = userId;
    public Rating Rating { get; private set; } = rating;
    public Comment Comment { get; private set; } = comment;
    public DateTime CreatedOnUtc { get; private set; } = createdOnUtc;
    
    public static Result<Review> Create(
        Booking booking,
        Rating rating,
        Comment comment,
        DateTime createdOnUtc)
    {
        if (booking.Status != BookingStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        var review = new Review(
            Guid.NewGuid(),
            booking.ApartmentId,
            booking.Id,
            booking.UserId,
            rating,
            comment,
            createdOnUtc);

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

        return Result.Success(review);
    }
}