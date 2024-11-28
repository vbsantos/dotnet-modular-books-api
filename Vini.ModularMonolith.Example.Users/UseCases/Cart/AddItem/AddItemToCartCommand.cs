using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Users.UseCases.Cart.AddItem;

public record AddItemToCartCommand(Guid BookId, int Quantity, string EmailAddress) : IRequest<Result>;
