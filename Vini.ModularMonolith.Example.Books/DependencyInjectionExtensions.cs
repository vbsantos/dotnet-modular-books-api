using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vini.ModularMonolith.Example.Books.Data;

namespace Vini.ModularMonolith.Example.Books;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddBookService(
    this IServiceCollection services,
    ConfigurationManager config
  )
  {
    var connectionString = config.GetConnectionString("BooksConnectionString");
    services.AddDbContext<BookDbContext>(options =>
      options.UseSqlServer(connectionString)
    );
    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IBookRepository, EFBookRepository>();
    return services;
  }
}
