using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Vini.ModularMonolith.Example.Books.Data;

public class BooksDbContext : DbContext
{
  internal DbSet<Book> Books { get; set; }

  public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("Books");
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    base.ConfigureConventions(configurationBuilder);
    configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
  }
}
