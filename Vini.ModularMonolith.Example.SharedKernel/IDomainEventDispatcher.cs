namespace Vini.ModularMonolith.Example.SharedKernel;

public interface IDomainEventDispatcher
{
  Task DispatchAndClearEvents(IEnumerable<IHaveDomainEvents> entitiesWithEvents);
}
