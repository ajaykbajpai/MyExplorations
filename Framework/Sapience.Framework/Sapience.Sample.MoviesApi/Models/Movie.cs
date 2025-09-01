namespace Sapience.Sample.MoviesApi.Models;

public record Movie
{
    public string Title { get; init; }
    public string Year { get; init; }
    public string Rated { get; init; }
    public string Released { get; init; }
    public string Runtime { get; init; }
    public string Genre { get; init; }
    public string Director { get; init; }
    public string Writer { get; init; }
    public string Actors { get; init; }
    public string Plot { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
    public string Awards { get; init; }
    public string Poster { get; init; }
    public List<Rating> Ratings { get; init; }
    public string Metascore { get; init; }
    public string imdbRating { get; init; }
    public string imdbVotes { get; init; }
    public string imdbID { get; init; }
    public string Type { get; init; }
    public string DVD { get; init; }
    public string BoxOffice { get; init; }
    public string Production { get; init; }
    public string Website { get; init; }
    public string Response { get; init; }
}