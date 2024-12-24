using MediatR;

namespace Vini.ModularMonolith.Example.OrderProcessing.Contracts;

public class OrderCreatedIntegrationEvent : INotification
{
  public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.UtcNow;
  public OrderDetailsDto OrderDetails { get; private set; }

  public OrderCreatedIntegrationEvent(OrderDetailsDto orderDetailsDto)
  {
    OrderDetails = orderDetailsDto;
  }
}
