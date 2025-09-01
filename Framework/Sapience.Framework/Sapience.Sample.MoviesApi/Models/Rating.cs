namespace Sapience.Sample.MoviesApi.Models;

public record Rating
{
    public string Source { get; init; }
    public string Value { get; init; }
}