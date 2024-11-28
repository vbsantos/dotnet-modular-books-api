using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Users.UseCases.Cart.Checkout;

public record CheckoutCartCommand(
  string EmailAddress,
  Guid ShippingAddressId,
  Guid BillingAddressId) : IRequest<Result<Guid>>;
