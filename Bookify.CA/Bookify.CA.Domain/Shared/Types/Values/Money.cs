namespace Bookify.CA.Domain.Shared.Types.Values;

public record Money(decimal Amount, Currency Currency)
{
    public static Money operator +(Money first, Money second)
    {
        if (first.Currency != second.Currency)
            throw new InvalidOperationException("Currencies to be added must be equal");
        
        return new Money(first.Amount + second.Amount, first.Currency);
    }
    
    public static Money Zero() => new(0m, Currency.None);
    public static Money Zero(Currency currency) => new(0m, currency);
    public bool IsZero() => this == Zero(Currency);
}