using Bookify.CA.Domain.Abstractions;

namespace Bookify.CA.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;