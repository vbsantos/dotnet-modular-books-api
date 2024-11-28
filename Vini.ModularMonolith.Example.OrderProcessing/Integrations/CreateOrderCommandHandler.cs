using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Contracts;
using Vini.ModularMonolith.Example.OrderProcessing.Data;

namespace Vini.ModularMonolith.Example.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
  private readonly IOrderRepository _orderRepository;
  private readonly ILogger<CreateOrderCommandHandler> _logger;

  public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
  {
    _orderRepository = orderRepository;
    _logger = logger;
  }

  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    var items = request.OrderItems.Select(oi => new OrderItem(
      oi.BookId,
      oi.Quantity,
      oi.UnitPrice,
      oi.Description
    ));

    //Fake Address
    var shippingAddress = new Address("123 Main", "", "Kent", "OH", "44444", "USA");
    var billingAddress = shippingAddress;

    var newOrder = Order.Factory.Create(
      userId: request.UserId,
      shippingAddress: shippingAddress,
      billingAddress: billingAddress,
      orderItems: items
    );

    await _orderRepository.AddAsync(newOrder);
    await _orderRepository.SaveChangesAsync();

    _logger.LogInformation("New Order ({orderId}) Created!", newOrder.Id);

    return new OrderDetailsResponse(newOrder.Id);
  }
}
