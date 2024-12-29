using Bookify.CA.Domain.Abstractions;
using Bookify.CA.Domain.Users.Events;
using Bookify.CA.Domain.Users.Types.Values;

namespace Bookify.CA.Domain.Users.Entities;

public sealed class User(
    Guid id,
    FirstName firstName,
    LastName lastName,
    Email email)
    : Entity(id)
{
    public FirstName FirstName { get; private set; } = firstName;
    public LastName LastName { get; private set; } = lastName;
    public Email Email { get; private set; } = email;

    public static Result<User> Create(FirstName firstName, LastName lastName, Email email)
    {
        User user = new(Guid.NewGuid(), firstName, lastName, email);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return Result.Success(user);
    }
}