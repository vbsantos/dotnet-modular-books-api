using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Vini.ModularMonolith.Example.EmailSending;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddEmailSendingModuleServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies)
  {
    // Add module services
    services.AddTransient<ISendEmail, MimeKitEmailSender>();

    // if using MediatR in this module, add any assemblies that contain handlers to the list
    mediatRAssemblies.Add(typeof(DependencyInjectionExtensions).Assembly);

    logger.Information("{Module} module services registered", "EmailSending");
    return services;
  }
}
