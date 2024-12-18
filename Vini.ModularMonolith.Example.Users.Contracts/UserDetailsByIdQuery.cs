using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Users.Contracts;

public record UserDetailsByIdQuery(Guid UserId) : IRequest<Result<UserDetails>>;
