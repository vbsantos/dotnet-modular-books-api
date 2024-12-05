namespace Vini.ModularMonolith.Example.Users;

internal sealed class AddressAddedEvent : DomainEventBase
{
  public UserStreetAddress NewAddress { get; }

  public AddressAddedEvent(UserStreetAddress newAddress)
  {
    NewAddress = newAddress;
  }
}
