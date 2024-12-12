using Vini.ModularMonolith.Example.SharedKernel;

namespace Vini.ModularMonolith.Example.Users.Domain;

internal sealed class AddressAddedEvent : DomainEventBase
{
  public UserStreetAddress NewAddress { get; }

  public AddressAddedEvent(UserStreetAddress newAddress)
  {
    NewAddress = newAddress;
  }
}
