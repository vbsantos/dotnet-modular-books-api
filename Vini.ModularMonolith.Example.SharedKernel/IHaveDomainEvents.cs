namespace Vini.ModularMonolith.Example.SharedKernel;

public interface IHaveDomainEvents
{
  IEnumerable<DomainEventBase> DomainEvents { get; }
  void ClearDomainEvents();
}
