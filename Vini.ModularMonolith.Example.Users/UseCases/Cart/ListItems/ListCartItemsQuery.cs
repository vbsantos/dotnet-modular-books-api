using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.Users.CartEndpoints;

namespace Vini.ModularMonolith.Example.Users.UseCases.Cart.ListItems;

public record ListCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;
