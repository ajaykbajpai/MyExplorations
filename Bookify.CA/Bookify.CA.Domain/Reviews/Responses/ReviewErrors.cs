using Bookify.CA.Domain.Abstractions;

namespace Bookify.CA.Domain.Reviews.Responses;

public static class ReviewErrors
{
    public static readonly Error NotEligible = new(
        "Review.NotEligible",
        "The review is not eligible because the booking is not yet completed");
}