using Bookify.CA.Domain.Abstractions;

namespace Bookify.CA.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;