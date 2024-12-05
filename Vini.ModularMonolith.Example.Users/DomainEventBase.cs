﻿using MediatR;

namespace Vini.ModularMonolith.Example.Users;

public abstract class DomainEventBase : INotification
{
  public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
