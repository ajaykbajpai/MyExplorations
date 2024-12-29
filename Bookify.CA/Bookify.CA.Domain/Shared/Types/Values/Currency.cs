using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Bookify.CA.Domain.Shared.Types.Values;

public record Currency(string Code)
{
    internal static readonly Currency None = new("");
    public static readonly IReadOnlyCollection<Currency> AllCurrencies = new List<Currency>()
    {
        new ("INR"),
        new ("EUR"),
        new ("USD")
    };

    public static Currency FromCode(string code) => AllCurrencies.FirstOrDefault(c => c.Code == code) ??
                                                    throw new ApplicationException("The currency code is invalid.");
}