namespace Bookify.CA.Domain.Apartments.Types.Values;

public sealed record Address(
    string Country,
    string State,
    string ZipCode,
    string City,
    string Street
);