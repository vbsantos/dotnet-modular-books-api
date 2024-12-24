using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Vini.ModularMonolith.Example.Reporting.Integrations;

namespace Vini.ModularMonolith.Example.Reporting;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddReportingModuleServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies
  )
  {
    // configure module services
    services.AddScoped<ITopSellingBooksReportService, TopSellingBooksReportService>();
    services.AddScoped<ISalesReportService, DefaultSalesReportService>();
    services.AddScoped<OrderIngestionService>();

    mediatRAssemblies.Add(typeof(DependencyInjectionExtensions).Assembly);

    logger.Information("{Module} module services registered.", "Reporting");

    return services;
  }
}
