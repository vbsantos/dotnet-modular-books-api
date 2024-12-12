using MediatR;

namespace Vini.ModularMonolith.Example.SharedKernel;

public abstract record IntegrationEventBase : INotification
{
  public DateTimeOffset DateTimeOffse { get; set; } = DateTimeOffset.UtcNow;
}
