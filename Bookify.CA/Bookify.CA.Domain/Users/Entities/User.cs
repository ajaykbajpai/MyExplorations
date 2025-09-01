using Bookify.CA.Domain.Abstractions;
using Bookify.CA.Domain.Users.Events;
using Bookify.CA.Domain.Users.Types.Values;

namespace Bookify.CA.Domain.Users.Entities;

public sealed class User : Entity
{
    private User(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }

    /// <summary>
    /// Create static factory method. This must be the only way to create a user.
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <returns>User Result</returns>
    public static Result<User> Create(FirstName firstName, LastName lastName, Email email)
    {
        User user = new(Guid.NewGuid(), firstName, lastName, email);
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return Result.Success(user);
    }
}