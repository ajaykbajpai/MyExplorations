using Bookify.CA.Domain.Abstractions;

namespace Bookify.CA.Domain.Reviews.Responses;

public static class RatingErrors
{
    public static readonly Error Invalid = new("Rating.Invalid", "The rating is invalid");
}