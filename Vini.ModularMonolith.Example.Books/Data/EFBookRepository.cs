using Microsoft.EntityFrameworkCore;

namespace Vini.ModularMonolith.Example.Books.Data;

internal class EFBookRepository : IBookRepository
{
  private readonly BookDbContext _dbContext;

  public EFBookRepository(BookDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public Task AddAsync(Book book)
  {
    _dbContext.Add(book);
    return Task.CompletedTask;
  }

  public Task DeleteAsync(Book book)
  {
    _dbContext.Remove(book);
    return Task.CompletedTask;
  }

  public async Task<Book?> GetByIdAsync(Guid id)
  {
    return await _dbContext.Books.FindAsync(id);
  }

  public async Task<List<Book>> ListAsync()
  {
    return await _dbContext.Books.ToListAsync();
  }

  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}
