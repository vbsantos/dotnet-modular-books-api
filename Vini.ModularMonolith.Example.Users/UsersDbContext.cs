using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Vini.ModularMonolith.Example.Users;

internal class UsersDbContext : IdentityDbContext
{
  public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
  {
  }

  public DbSet<ApplicationUser> ApplicationUsers { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema("Users");

    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(builder);
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
  }
}
