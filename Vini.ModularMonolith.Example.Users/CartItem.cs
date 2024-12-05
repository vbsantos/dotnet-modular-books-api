using Ardalis.GuardClauses;

namespace Vini.ModularMonolith.Example.Users;

public class CartItem
{
  public CartItem(Guid bookId, string description, int quantity, decimal unitPrice)
  {
    BookId = Guard.Against.Default(bookId);
    Description = Guard.Against.NullOrEmpty(description);
    Quantity = Guard.Against.NegativeOrZero(quantity);
    UnitPrice = Guard.Against.Negative(unitPrice);
  }

  public Guid Id { get; private set; } = Guid.NewGuid();
  public Guid BookId { get; private set; }
  public string Description { get; set; } = string.Empty;
  public int Quantity { get; private set; }
  public decimal UnitPrice { get; private set; }

  public void UpdateQuantity(int quantity)
  {
    Quantity = Guard.Against.NegativeOrZero(quantity);
  }

  public void UpdateDescription(string description)
  {
    Description = description;
  }

  public void UpdateUnitPrice(decimal unitPrice)
  {
    UnitPrice = unitPrice;
  }
}
