using Microsoft.EntityFrameworkCore;

namespace Vini.ModularMonolith.Example.Users.Data;

internal class EfApplicationUserRepository : IApplicationUserRepository
{
  private readonly UsersDbContext _dbContext;

  public EfApplicationUserRepository(UsersDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<ApplicationUser> GetUserWithCartByEmailAsync(string email)
  {
    var user = await _dbContext.ApplicationUsers
      .Include(u => u.CartItems)
      .SingleAsync(u => u.Email == email);

    return user;
  }

  public Task SaveChangesAsync()
  {
    return _dbContext.SaveChangesAsync();
  }
}
