using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using Scalar.AspNetCore;
using Serilog;
using Vini.ModularMonolith.Example.Books;
using Vini.ModularMonolith.Example.Users;

var logger = Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.Console()
  .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);
{
  builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

  builder.Services.AddOpenApi();
  builder.Services
    .AddAuthenticationJwtBearer(bearerOptions => bearerOptions.SigningKey = builder.Configuration["Auth:JwtSecret"])
    .AddAuthorization()
    .AddFastEndpoints();

  // Module Services
  List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];
  builder.Services.AddBookService(builder.Configuration, logger, mediatRAssemblies);
  builder.Services.AddUserService(builder.Configuration, logger, mediatRAssemblies);

  // Set up MediatR
  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies([.. mediatRAssemblies]));
}

var app = builder.Build();
{
  if (app.Environment.IsDevelopment())
  {
    app.MapScalarApiReference();
    app.MapOpenApi();
  }

  app.UseAuthentication();
  app.UseAuthorization();

  app.UseFastEndpoints();

  app.Run();
}

public partial class Program { } // Required for Tests
