using Ardalis.GuardClauses;

namespace Vini.ModularMonolith.Example.Users.Domain;

public class UserStreetAddress
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public string UserId { get; private set; } = string.Empty;
  public Address StreetAddress { get; private set; } = default!;

  public UserStreetAddress() { } // EF

  public UserStreetAddress(string userId, Address streetAddress)
  {
    UserId = Guard.Against.NullOrEmpty(userId);
    StreetAddress = Guard.Against.Null(streetAddress);
  }
}
