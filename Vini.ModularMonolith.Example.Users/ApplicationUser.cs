using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace Vini.ModularMonolith.Example.Users;

public class ApplicationUser : IdentityUser
{
  public string FullName { get; set; } = string.Empty;
  private readonly List<CartItem> _cartItems = [];
  public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

  private readonly List<UserStreetAddress> _addresses = [];
  public IReadOnlyCollection<UserStreetAddress> Addresses => _addresses.AsReadOnly();

  public void AddItemToCart(CartItem item)
  {
    Guard.Against.Null(item);

    var existingBook = _cartItems.SingleOrDefault(c => c.BookId == item.BookId);
    if (existingBook != null)
    {
      existingBook.UpdateQuantity(existingBook.Quantity + item.Quantity);
      existingBook.UpdateDescription(item.Description);
      existingBook.UpdateUnitPrice(item.UnitPrice);

      return;
    }
    _cartItems.Add(item);
  }

  internal UserStreetAddress AddAddress(Address address)
  {
    Guard.Against.Null(address);

    // find existing address and just return it
    var existingAddress = _addresses.SingleOrDefault(a => a.StreetAddress == address);
    if (existingAddress != null)
    {
      return existingAddress;
    }

    var newAddress = new UserStreetAddress(Id, address);
    _addresses.Add(newAddress);

    return newAddress;
  }

  public void ClearCart()
  {
    _cartItems.Clear();
  }
}
