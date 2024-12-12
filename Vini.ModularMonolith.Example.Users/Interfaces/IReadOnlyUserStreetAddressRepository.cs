using Vini.ModularMonolith.Example.Users.Domain;

namespace Vini.ModularMonolith.Example.Users.Interfaces;

public interface IReadOnlyUserStreetAddressRepository
{
  Task<UserStreetAddress?> GetById(Guid userStreetAddressId);
}
