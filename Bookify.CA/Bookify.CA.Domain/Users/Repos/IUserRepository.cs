using Bookify.CA.Domain.Users.Entities;

namespace Bookify.CA.Domain.Users.Repos;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(User user);
}