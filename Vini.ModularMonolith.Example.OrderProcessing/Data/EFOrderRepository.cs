using Microsoft.EntityFrameworkCore;

namespace Vini.ModularMonolith.Example.OrderProcessing.Data;

internal class EFOrderRepository : IOrderRepository
{
  private readonly OrderProcessingDbContext _dbContext;

  public EFOrderRepository(OrderProcessingDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task AddAsync(Order order)
  {
    await _dbContext.Orders.AddAsync(order);
  }

  public async Task<List<Order>> ListAsync()
  {
    // For a much more flexible way to work with repositories see about the Specification pattern
    return await _dbContext.Orders
      .Include(o => o.OrderItems)
      .ToListAsync();
  }

  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}
