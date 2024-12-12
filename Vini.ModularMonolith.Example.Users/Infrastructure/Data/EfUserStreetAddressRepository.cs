using Microsoft.EntityFrameworkCore;
using Vini.ModularMonolith.Example.Users.Domain;
using Vini.ModularMonolith.Example.Users.Interfaces;

namespace Vini.ModularMonolith.Example.Users.Infrastructure.Data;

internal class EfUserStreetAddressRepository : IReadOnlyUserStreetAddressRepository
{
  private readonly UsersDbContext _dbContext;

  public EfUserStreetAddressRepository(UsersDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public Task<UserStreetAddress?> GetById(Guid userStreetAddressId)
  {
    return _dbContext.UserStreetAddresses.SingleOrDefaultAsync(a => a.Id == userStreetAddressId);
  }
}
