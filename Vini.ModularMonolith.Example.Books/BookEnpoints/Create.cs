using FastEndpoints;

namespace Vini.ModularMonolith.Example.Books.BookEnpoints;

internal class Create(IBookService bookService) : Endpoint<CreateBookRequest, BookDto>
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Post("/books");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
  {
    var newBookDto = new BookDto(req.Id ?? Guid.NewGuid(), req.Title, req.Author, req.Price);

    await _bookService.CreateBookAsync(newBookDto);

    await SendCreatedAtAsync<GetById>(new { newBookDto.Id }, newBookDto, cancellation: ct);
  }
}
