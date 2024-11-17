namespace Vini.ModularMonolith.Example.Users.CartEndpoints;

public record AddCartItemRequest(Guid BookId, int Quantity);
