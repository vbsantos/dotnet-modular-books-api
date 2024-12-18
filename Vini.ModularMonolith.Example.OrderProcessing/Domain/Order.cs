using System.ComponentModel.DataAnnotations.Schema;
using Vini.ModularMonolith.Example.SharedKernel;

namespace Vini.ModularMonolith.Example.OrderProcessing.Domain;

internal class Order : IHaveDomainEvents
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public Guid UserId { get; private set; }
  public Address ShippingAddress { get; private set; } = default!;
  public Address BillingAddress { get; private set; } = default!;
  private readonly List<OrderItem> _orderItems = new();
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

  public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.Now;

  private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

  #region DomainEvent

  private List<DomainEventBase> _domainEvents = [];
  [NotMapped]
  public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

  protected void RegisterDomainEvents(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  void IHaveDomainEvents.ClearDomainEvents() => _domainEvents.Clear();

  #endregion

  internal class Factory
  {
    public static Order Create(
      Guid userId,
      Address shippingAddress,
      Address billingAddress,
      IEnumerable<OrderItem> orderItems)
    {
      var order = new Order();
      order.UserId = userId;
      order.ShippingAddress = shippingAddress;
      order.BillingAddress = billingAddress;
      foreach (var item in orderItems)
      {
        order.AddOrderItem(item);
      }

      var createdEvent = new OrderCreatedEvent(order);
      order.RegisterDomainEvents(createdEvent);

      return order;
    }
  }
}
