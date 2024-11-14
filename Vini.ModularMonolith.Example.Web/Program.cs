using FastEndpoints;
using Scalar.AspNetCore;
using Vini.ModularMonolith.Example.Books;
using Vini.ModularMonolith.Example.Users;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services.AddOpenApi();
  builder.Services.AddFastEndpoints();

  // Module Services
  builder.Services.AddBookService(builder.Configuration);
  builder.Services.AddUserService(builder.Configuration);
}

var app = builder.Build();
{
  if (app.Environment.IsDevelopment())
  {
    app.MapScalarApiReference();
    app.MapOpenApi();
  }

  app.UseHttpsRedirection();

  app.UseFastEndpoints();

  app.Run();
}

public partial class Program { } // Required for Tests
