namespace Bookify.CA.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.Users.Types.Values.Email recipient, string subject, string body);
}