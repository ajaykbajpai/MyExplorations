using Microsoft.Extensions.Caching.Hybrid;
using Sapience.Sample.MoviesApi.Clients;
using Sapience.Sample.MoviesApi.Models;

namespace Sapience.Sample.MoviesApi.Movies;

internal sealed record GetMovieRequest(string ImdbId) : IRequest<Movie?>;

internal sealed class GetMovieRequestHandler(
    ImdbApiClient client,
    HybridCache hybridCache
    ) : IRequestHandler<GetMovieRequest, Movie?>
{
    public async Task<Movie?> Handle(GetMovieRequest request, CancellationToken cancellationToken)
    {
        var movie = await hybridCache.GetOrCreateAsync($"movie:{request.ImdbId}", async entry =>
            {
                var movie = await client.GetMovieByImdbIdAsync(request.ImdbId, entry);
                return movie;
            },
            tags: ["movies"],
            cancellationToken: cancellationToken);
        return movie;
    }
}