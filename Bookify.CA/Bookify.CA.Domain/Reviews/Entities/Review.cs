using Bookify.CA.Domain.Abstractions;
using Bookify.CA.Domain.Bookings.Entities;
using Bookify.CA.Domain.Bookings.Types.Values;
using Bookify.CA.Domain.Reviews.Events;
using Bookify.CA.Domain.Reviews.Responses;
using Bookify.CA.Domain.Reviews.Types.Values;

namespace Bookify.CA.Domain.Reviews.Entities;

public sealed class Review : Entity
{
    private Review(
        Guid id,
        Guid apartmentId,
        Guid bookingId,
        Guid userId,
        Rating rating,
        Comment comment,
        DateTime createdOnUtc) : base(id)
    {
        ApartmentId = apartmentId;
        BookingId = bookingId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid ApartmentId { get; private set; }
    public Guid BookingId { get; private set; }
    public Guid UserId { get; private set; }
    public Rating Rating { get; private set; }
    public Comment Comment { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    /// <summary>
    /// Create static factory method. This must be the only way to create a review.
    /// </summary>
    /// <param name="booking"></param>
    /// <param name="rating"></param>
    /// <param name="comment"></param>
    /// <param name="createdOnUtc"></param>
    /// <returns>Review Result</returns>
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

        Review review = new(
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