using Microsoft.AspNetCore.Builder;

namespace Vini.ModularMonolith.Example.Books;

public static class BookEndpoins
{
    public static void MapBooksEndpoints(this WebApplication app)
    {
        app.MapGet("/books", (IBookService bookService) =>
        {
            return bookService.ListBooks();
        });
    }
}
