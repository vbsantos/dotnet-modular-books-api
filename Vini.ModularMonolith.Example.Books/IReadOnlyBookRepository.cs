namespace Vini.ModularMonolith.Example.Books;

internal interface IReadOnlyBookRepository
{
  Task<Book?> GetByIdAsync(Guid id);
  Task<List<Book>> ListAsync();
}
