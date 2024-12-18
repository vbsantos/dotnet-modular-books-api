using MediatR;
using Vini.ModularMonolith.Example.EmailSending.Contracts;
using Vini.ModularMonolith.Example.Users.Contracts;

namespace Vini.ModularMonolith.Example.OrderProcessing.Domain;

internal class SendConfirmationEmailOrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
  private readonly IMediator _mediator;

  public SendConfirmationEmailOrderCreatedEventHandler(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task Handle(OrderCreatedEvent notification, CancellationToken ct)
  {
    var userByIdQuery = new UserDetailsByIdQuery(notification.Order.UserId);

    var userDetails = await _mediator.Send(userByIdQuery, ct);

    if (!userDetails.IsSuccess)
    {
      //TODO: Log the error
      return;
    }

    var itemQuantity = notification.Order.OrderItems.Sum(orderItem => orderItem.Quantity);

    var command = new SendEmailCommand
    {
      To = userDetails.Value.EmailAddress,
      From = "noreply@test.com",
      Subject = "Your ModularMonolith Books Purchase",
      Body = $"You bought {itemQuantity} items."
    };

    var emailId = await _mediator.Send(command, ct);

    // TODO: Do something with emailId
  }
}
