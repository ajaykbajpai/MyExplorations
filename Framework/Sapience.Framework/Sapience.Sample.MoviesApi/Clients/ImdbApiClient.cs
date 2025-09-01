using Microsoft.Extensions.Options;
using Sapience.Sample.MoviesApi.Configuration;
using Sapience.Sample.MoviesApi.Models;

namespace Sapience.Sample.MoviesApi.Clients;

internal sealed class ImdbApiClient(HttpClient httpClient, IOptions<ImdbApiSettings> settings)
{
    private readonly string _apiKey = settings.Value.ApiKey;

    public async Task<Movie?> GetMovieByImdbIdAsync(string imdbId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetFromJsonAsync<Movie>(
            $"?apikey={_apiKey}&i={imdbId}",
            cancellationToken);

        return response;
    }
}