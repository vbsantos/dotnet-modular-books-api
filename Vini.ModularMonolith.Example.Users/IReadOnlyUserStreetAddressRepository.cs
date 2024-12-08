namespace Vini.ModularMonolith.Example.Users;

public interface IReadOnlyUserStreetAddressRepository
{
  Task<UserStreetAddress?> GetById(Guid userStreetAddressId);
}
