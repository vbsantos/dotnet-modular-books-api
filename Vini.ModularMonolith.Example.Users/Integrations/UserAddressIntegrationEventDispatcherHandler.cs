using MediatR;
using Microsoft.Extensions.Logging;
using Users.Contracts;

namespace Vini.ModularMonolith.Example.Users.Integrations;

internal class UserAddressIntegrationEventDispatcherHandler : INotificationHandler<AddressAddedEvent>
{
  private readonly ILogger<UserAddressIntegrationEventDispatcherHandler> _logger;
  private readonly IMediator _mediator;

  public UserAddressIntegrationEventDispatcherHandler(
    ILogger<UserAddressIntegrationEventDispatcherHandler> logger,
    IMediator mediator)
  {
    _logger = logger;
    _mediator = mediator;
  }

  public async Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
  {
    Guid userId = Guid.Parse(notification.NewAddress.UserId);

    var addressDetails = new UserAddressDetails(userId,
      notification.NewAddress.Id,
      notification.NewAddress.StreetAddress.Street1,
      notification.NewAddress.StreetAddress.Street2,
      notification.NewAddress.StreetAddress.City,
      notification.NewAddress.StreetAddress.State,
      notification.NewAddress.StreetAddress.PostalCode,
      notification.NewAddress.StreetAddress.Country
    );

    await _mediator.Publish(new NewUserAddressAddedIntegrationEvent(addressDetails), cancellationToken);

    _logger.LogInformation("New address added to user {user}: {address}",
      notification.NewAddress.UserId,
      notification.NewAddress.StreetAddress);
  }
}
