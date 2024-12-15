using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Users.UseCases.User.Create;

internal record CreateUserCommand(string Email, string Password) : IRequest<Result>;
