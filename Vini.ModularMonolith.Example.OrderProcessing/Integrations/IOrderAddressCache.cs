using Ardalis.Result;

namespace Vini.ModularMonolith.Example.OrderProcessing.Integrations;

internal interface IOrderAddressCache
{
  Task<Result<OrderAddress>> GetByIdAsync(Guid addressId);
  Task<Result> StoreAsync(OrderAddress orderAddress);
}
