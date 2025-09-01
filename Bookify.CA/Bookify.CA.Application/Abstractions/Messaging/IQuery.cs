using Bookify.CA.Domain.Abstractions;
using MediatR;

namespace Bookify.CA.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}