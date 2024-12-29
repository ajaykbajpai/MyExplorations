using Bookify.CA.Domain.Abstractions;
using Bookify.CA.Domain.Reviews.Responses;

namespace Bookify.CA.Domain.Reviews.Types.Values;

public sealed record Rating
{
    private Rating(int value) => Value = value;

    public int Value { get; init; }

    public static Result<Rating> Create(int value)
    {
        if (value is < 1 or > 5)
        {
            return Result.Failure<Rating>(RatingErrors.Invalid);
        }

        return Result.Success(new Rating(value));
    }
}