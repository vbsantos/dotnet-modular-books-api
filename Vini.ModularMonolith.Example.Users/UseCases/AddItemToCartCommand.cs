using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Users.UseCases;

public record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress) : IRequest<Result>;
