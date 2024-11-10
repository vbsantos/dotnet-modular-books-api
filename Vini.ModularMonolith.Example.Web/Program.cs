using FastEndpoints;

using Vini.ModularMonolith.Example.Books;

var builder = WebApplication.CreateBuilder(args);
{
  builder.Services.AddOpenApi();
  builder.Services.AddFastEndpoints();
  builder.Services.AddBookService();
}

var app = builder.Build();
{
  if (app.Environment.IsDevelopment())
  {
    app.MapOpenApi();
  }

  app.UseHttpsRedirection();

  app.UseFastEndpoints();

  app.Run();
}
