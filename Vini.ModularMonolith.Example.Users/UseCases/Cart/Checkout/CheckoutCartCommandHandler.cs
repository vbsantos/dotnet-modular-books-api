using Ardalis.Result;
using MediatR;
using OrderProcessing.Contracts;

namespace Vini.ModularMonolith.Example.Users.UseCases.Cart.Checkout;

internal class CheckoutCartCommandHandler : IRequestHandler<CheckoutCartCommand, Result<Guid>>
{
  private readonly IApplicationUserRepository _userRepository;
  private readonly IMediator _mediator;

  public CheckoutCartCommandHandler(IApplicationUserRepository userRepository, IMediator mediator)
  {
    _userRepository = userRepository;
    _mediator = mediator;
  }

  public async Task<Result<Guid>> Handle(CheckoutCartCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithCartByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    var items = user.CartItems.Select(item => new OrderItemDetails(
      item.BookId,
      item.Quantity,
      item.UnitPrice,
      item.Description
    )).ToList();

    var createOrderCommand = new CreateOrderCommand(
      Guid.Parse(user.Id),
      request.ShippingAddressId,
      request.BillingAddressId,
      items
    );

    //TODO: Consider replacing with a message-based approach for perf reasons
    var result = await _mediator.Send(createOrderCommand, cancellationToken); // synchronous

    if (!result.IsSuccess)
    {
      // Change from a Result<OrderDetailsResponse> to Result<Guid>
      return result.Map(x => x.OrderId);
    }

    user.ClearCart();
    await _userRepository.SaveChangesAsync();

    // send email to customer
    // TODO: do this in an event handler
    //var command = new SendEmailCommand();

    return Result.Success(result.Value.OrderId);
  }
}
