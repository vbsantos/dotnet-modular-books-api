using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Vini.ModularMonolith.Example.Books.Data;

namespace Vini.ModularMonolith.Example.Books;

public static class DependencyInjectionExtensions
{
  public static IServiceCollection AddBookService(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger,
    List<System.Reflection.Assembly> mediatRAssemblies
  )
  {
    var connectionString = config.GetConnectionString("BooksConnectionString");
    services.AddDbContext<BooksDbContext>(options =>
      options.UseSqlServer(connectionString)
    );
    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IBookRepository, EFBookRepository>();

    mediatRAssemblies.Add(typeof(DependencyInjectionExtensions).Assembly);

    logger.Information("{Module} module services registered.", "Books");

    return services;
  }
}
