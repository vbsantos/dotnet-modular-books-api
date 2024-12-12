using Vini.ModularMonolith.Example.OrderProcessing.Domain;

namespace Vini.ModularMonolith.Example.OrderProcessing.Interfaces;

internal interface IOrderRepository
{
  Task AddAsync(Order order);
  Task<List<Order>> ListAsync();
  Task SaveChangesAsync();
}
