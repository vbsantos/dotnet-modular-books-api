using Vini.ModularMonolith.Example.SharedKernel;

namespace Vini.ModularMonolith.Example.OrderProcessing.Domain;

internal class OrderCreatedEvent : DomainEventBase
{
  public OrderCreatedEvent(Order order)
  {
    Order = order;
  }

  public Order Order { get; }
}
