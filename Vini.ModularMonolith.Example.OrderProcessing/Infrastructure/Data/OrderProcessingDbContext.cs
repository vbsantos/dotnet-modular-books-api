using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Vini.ModularMonolith.Example.OrderProcessing.Domain;
using Vini.ModularMonolith.Example.SharedKernel;

namespace Vini.ModularMonolith.Example.OrderProcessing.Infrastructure.Data;

internal class OrderProcessingDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public OrderProcessingDbContext(
    DbContextOptions<OrderProcessingDbContext> options,
    IDomainEventDispatcher? dispatcher) : base(options)
  {
    _dispatcher = dispatcher;
  }

  public DbSet<Order> Orders { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.HasDefaultSchema("OrderProcessing");

    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(builder);
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null)
    {
      return result;
    }

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<IHaveDomainEvents>()
    .Select(e => e.Entity)
      .Where(e => e.DomainEvents.Any())
    .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }
}
