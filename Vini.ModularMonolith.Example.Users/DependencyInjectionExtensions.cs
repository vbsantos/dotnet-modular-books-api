using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Vini.ModularMonolith.Example.Users.Domain;
using Vini.ModularMonolith.Example.Users.Infrastructure.Data;
using Vini.ModularMonolith.Example.Users.Interfaces;

namespace Vini.ModularMonolith.Example.Users;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddUserModuleService(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies
  )
  {
    var connectionString = config.GetConnectionString("UsersConnectionString");
    services.AddDbContext<UsersDbContext>(options =>
      options.UseSqlServer(connectionString)
    );

    services.AddIdentityCore<ApplicationUser>()
      .AddEntityFrameworkStores<UsersDbContext>();

    services.AddScoped<IApplicationUserRepository, EFApplicationUserRepository>();
    services.AddScoped<IReadOnlyUserStreetAddressRepository, EfUserStreetAddressRepository>();

    mediatRAssemblies.Add(typeof(DependencyInjectionExtensions).Assembly);

    logger.Information("{Module} module services registered.", "Users");

    return services;
  }
}
