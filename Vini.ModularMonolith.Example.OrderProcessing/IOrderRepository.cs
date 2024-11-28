namespace Vini.ModularMonolith.Example.OrderProcessing.Data;

internal interface IOrderRepository
{
  Task AddAsync(Order order);
  Task<List<Order>> ListAsync();
  Task SaveChangesAsync();
}
