using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Users.UseCases.User.GetById;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDTO>>;
