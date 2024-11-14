using FastEndpoints;

namespace Vini.ModularMonolith.Example.Books.BookEnpoints;

internal class GetById(IBookService bookService) : Endpoint<GetBookByIdRequest, BookDto>
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Get("/books/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct = default)
  {
    var book = await _bookService.GetBookByIdAsync(req.Id);

    if (book is null)
    {
      await SendNotFoundAsync(ct);
      return;
    }

    await SendAsync(book, cancellation: ct);
  }
}
