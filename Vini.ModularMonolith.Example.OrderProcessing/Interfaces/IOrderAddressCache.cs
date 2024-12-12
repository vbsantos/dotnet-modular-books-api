using Ardalis.Result;
using Vini.ModularMonolith.Example.OrderProcessing.Infrastructure.Data;

namespace Vini.ModularMonolith.Example.OrderProcessing.Interfaces;

internal interface IOrderAddressCache
{
  Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
  Task<Result> StoreAsync(OrderAddress orderAddress);
}
