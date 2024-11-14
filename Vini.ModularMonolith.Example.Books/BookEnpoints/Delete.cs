using FastEndpoints;

namespace Vini.ModularMonolith.Example.Books.BookEnpoints;

internal class Delete(IBookService bookService) : Endpoint<DeleteBookRequest>
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Delete("/books/{id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct = default)
  {
    //TODO: Handle not found

    await _bookService.DeleteBookAsync(req.Id);

    await SendNoContentAsync(ct);
  }
}
