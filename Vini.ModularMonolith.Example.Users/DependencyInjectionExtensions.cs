using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Vini.ModularMonolith.Example.Users.Data;

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

    services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

    mediatRAssemblies.Add(typeof(DependencyInjectionExtensions).Assembly);

    logger.Information("{Module} module services registered.", "Users");

    return services;
  }
}
