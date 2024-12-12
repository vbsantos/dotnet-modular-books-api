using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Vini.ModularMonolith.Example.OrderProcessing.Domain;

namespace Vini.ModularMonolith.Example.OrderProcessing.Infrastructure.Data;

internal class OrderProcessingDbContext : DbContext
{
  public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options) : base(options)
  {
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
}
