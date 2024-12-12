using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using OrderProcessing.Contracts;
using Vini.ModularMonolith.Example.OrderProcessing.Domain;
using Vini.ModularMonolith.Example.OrderProcessing.Interfaces;

namespace Vini.ModularMonolith.Example.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
  private readonly IOrderRepository _orderRepository;
  private readonly ILogger<CreateOrderCommandHandler> _logger;
  private readonly IOrderAddressCache _addressCache;

  public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger, IOrderAddressCache addressCache)
  {
    _orderRepository = orderRepository;
    _logger = logger;
    _addressCache = addressCache;
  }

  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    var items = request.OrderItems.Select(oi => new OrderItem(
      oi.BookId,
      oi.Quantity,
      oi.UnitPrice,
      oi.Description
    ));

    // Materialize View - Using Redis
    var shippingAddress = await _addressCache.GetByIdAsync(request.ShippingAddressId);
    var billingAddress = await _addressCache.GetByIdAsync(request.BillingAddressId);

    var newOrder = Order.Factory.Create(
      userId: request.UserId,
      shippingAddress: shippingAddress.Value.Address,
      billingAddress: billingAddress.Value.Address,
      orderItems: items
    );

    await _orderRepository.AddAsync(newOrder);
    await _orderRepository.SaveChangesAsync();

    _logger.LogInformation("New Order ({orderId}) Created!", newOrder.Id);

    return new OrderDetailsResponse(newOrder.Id);
  }
}
