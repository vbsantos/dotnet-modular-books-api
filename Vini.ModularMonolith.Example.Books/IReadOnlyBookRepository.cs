namespace Vini.ModularMonolith.Example.Books;

public interface IReadOnlyBookRepository
{
  Task<Book?> GetByIdAsync(Guid id);
  Task<List<Book>> ListAsync();
}
