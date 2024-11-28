namespace Vini.ModularMonolith.Example.Users;

public interface IApplicationUserRepository
{
  Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
  Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
  Task SaveChangesAsync();
}
