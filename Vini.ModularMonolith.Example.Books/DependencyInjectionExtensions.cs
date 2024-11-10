using Microsoft.Extensions.DependencyInjection;

namespace Vini.ModularMonolith.Example.Books;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBookService(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}
