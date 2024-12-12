using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Vini.ModularMonolith.Example.OrderProcessing.Infrastructure;
using Vini.ModularMonolith.Example.OrderProcessing.Infrastructure.Data;
using Vini.ModularMonolith.Example.OrderProcessing.Interfaces;

namespace Vini.ModularMonolith.Example.OrderProcessing;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddOrderProcessingModuleService(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies
  )
  {
    var connectionString = config.GetConnectionString("OrderProcessingConnectionString");
    services.AddDbContext<OrderProcessingDbContext>(options =>
      options.UseSqlServer(connectionString)
    );

    services.AddScoped<IOrderRepository, EFOrderRepository>();
    services.AddScoped<RedisOrderAddressCache>();
    services.AddScoped<IOrderAddressCache, ReadThroughOrderAddressCache>();

    mediatRAssemblies.Add(typeof(DependencyInjectionExtensions).Assembly);

    logger.Information("{Module} module services registered.", "OrderProcessing");

    return services;
  }
}
