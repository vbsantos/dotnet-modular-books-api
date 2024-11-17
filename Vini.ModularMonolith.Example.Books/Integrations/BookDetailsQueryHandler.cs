using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.Books.Contracts;

namespace Vini.ModularMonolith.Example.Books.Integrations;

public class BookDetailsQueryHandler : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
{
  private readonly IBookRepository _bookRepository;

  public BookDetailsQueryHandler(IBookRepository bookRepository)
  {
    _bookRepository = bookRepository;
  }

  public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
  {
    var book = await _bookRepository.GetByIdAsync(request.BookId);

    if (book is null)
    {
      return Result.NotFound();
    }

    var response = new BookDetailsResponse(book.Id, book.Title, book.Author, book.Price);

    return response;
  }
}
