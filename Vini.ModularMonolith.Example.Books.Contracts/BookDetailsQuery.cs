using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Books.Contracts;

public record BookDetailsQuery(Guid BookId) : IRequest<Result<BookDetailsResponse>>;
