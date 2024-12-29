using Bookify.CA.Domain.Apartments.Entities;

namespace Bookify.CA.Domain.Apartments.Repos;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}