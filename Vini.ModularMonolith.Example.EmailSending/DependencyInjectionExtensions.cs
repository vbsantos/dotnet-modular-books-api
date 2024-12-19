﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
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
    // configure MongoDB
    services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
    services.AddMongoDB(config);

    // Add module services
    services.AddTransient<ISendEmail, MimeKitEmailSender>();
    services.AddTransient<IOutboxService, MongoDbOutboxService>();
    services.AddTransient<ISendEmailsFromOutboxService, DefaultSendEmailsFromOutboxService>();

    // if using MediatR in this module, add any assemblies that contain handlers to the list
    mediatRAssemblies.Add(typeof(DependencyInjectionExtensions).Assembly);

    // Add BackgroundWorker
    services.AddHostedService<EmailSendingBackgroundService>();

    logger.Information("{Module} module services registered", "EmailSending");
    return services;
  }

  private static IServiceCollection AddMongoDB(
    this IServiceCollection services,
    IConfiguration configuration)
  {
    // Register the MongoDB client as a singleton
    services.AddSingleton<IMongoClient>(serviceProvider =>
    {
      var settings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();
      return new MongoClient(settings!.ConnectionString);
    });

    // Register the MongoDB database as a singleton
    services.AddSingleton(serviceProvider =>
    {
      var settings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();
      var client = serviceProvider.GetRequiredService<IMongoClient>();
      return client.GetDatabase(settings!.DatabaseName);
    });

    // Optionally, register specific collections here as scoped or singleton services
    // Example for 'EmailOutboxEntity' collection
    services.AddTransient(serviceProvider =>
    {
      var database = serviceProvider.GetRequiredService<IMongoDatabase>();
      return database.GetCollection<EmailOutboxEntity>("EmailOutboxEntityCollection");
    });

    return services;
  }
}
