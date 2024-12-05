using MediatR;

namespace Users.Contracts;

public abstract record IntegrationEventBase : INotification
{
  public DateTimeOffset DateTimeOffse { get; set; } = DateTimeOffset.UtcNow;
}
