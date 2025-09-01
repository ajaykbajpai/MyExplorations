namespace Sapience.Sample.MoviesApi.Models;

public record MovieResponse
{
    public required Movie Movie { get; init; }
    public required int ApiRequestCount { get; init; }
}