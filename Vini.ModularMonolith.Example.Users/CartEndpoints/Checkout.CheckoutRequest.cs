namespace Vini.ModularMonolith.Example.Users.CartEndpoints;

public record CheckoutRequest(Guid ShippingAddressId, Guid BillingAddressId);
