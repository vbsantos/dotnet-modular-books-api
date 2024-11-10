using FastEndpoints;

namespace Vini.ModularMonolith.Example.Books;

internal class ListBooksendpoint(IBookService bookService) : EndpointWithoutRequest<ListBooksReponse>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        var books = _bookService.ListBooks();

        await SendAsync(new ListBooksReponse(Books: books), cancellation: cancellationToken);
    }
}
