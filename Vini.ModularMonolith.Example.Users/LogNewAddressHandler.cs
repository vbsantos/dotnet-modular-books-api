using MediatR;
using Microsoft.Extensions.Logging;

namespace Vini.ModularMonolith.Example.Users;

internal class LogNewAddressHandler : INotificationHandler<AddressAddedEvent>
{
  private readonly ILogger<LogNewAddressHandler> _logger;

  public LogNewAddressHandler(ILogger<LogNewAddressHandler> logger)
  {
    _logger = logger;
  }

  public Task Handle(AddressAddedEvent notification, CancellationToken cancellationToken)
  {
    _logger.LogInformation("[DE Handler] New address added to user {user}: {address}",
      notification.NewAddress.UserId,
      notification.NewAddress.StreetAddress);

    return Task.CompletedTask;
  }
}
