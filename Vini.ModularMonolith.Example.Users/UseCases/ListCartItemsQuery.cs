using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Users.UseCases;

public record ListCartItemsQuery(string EmailAddress) : IRequest<Result<List<CartItemDto>>>;
