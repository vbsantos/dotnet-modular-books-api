using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vini.ModularMonolith.Example.Users;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddUserService(
    this IServiceCollection services,
    ConfigurationManager config
  )
  {
    var connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options =>
      options.UseSqlServer(connectionString)
    );

    services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();

    return services;
  }
}
