namespace Vini.ModularMonolith.Example.Users;

public record CartItemDto(Guid Id, Guid BookId, string Description, int Quantity, decimal UnitPrice);
