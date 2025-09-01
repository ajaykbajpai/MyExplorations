using Microsoft.Extensions.Caching.Hybrid;
using Sapience.Sample.MoviesApi.Clients;
using Sapience.Sample.MoviesApi.Configuration;
using Sapience.Sample.MoviesApi.Models;
using Sapience.Sample.MoviesApi.Movies;

var builder = WebApplication.CreateBuilder(args);
// builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<ImdbApiSettings>(builder.Configuration.GetSection("OmdbApi"));

builder.Services.AddHttpClient<ImdbApiClient>(client =>
{
    var settings = builder.Configuration.GetSection("ImdbApi").Get<ImdbApiSettings>();
    client.BaseAddress =
        new Uri(settings?.BaseUrl ?? throw new InvalidOperationException("IMDB API Base URL is not configured"));
});

builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        LocalCacheExpiration = TimeSpan.FromMinutes(1),
        Expiration = TimeSpan.FromMinutes(5)
    };
});

builder.Services.AddScoped<GetMovieRequestHandler>();
builder.Services.AddScoped<IRequestHandler<GetMovieRequest, Movie?>>(sp =>
    new LoggingRequestHandler<GetMovieRequest, Movie?>(
        sp.GetRequiredService<GetMovieRequestHandler>(),
        sp.GetRequiredService<ILogger<LoggingRequestHandler<GetMovieRequest, Movie?>>>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}