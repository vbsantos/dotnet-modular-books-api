using OrderProcessing.Contracts;

namespace Vini.ModularMonolith.Example.OrderProcessing.Contracts;

/// <summary>
/// Basic Details of the order
/// TODO: Include address info to be used by geographic spacific reports
/// </summary>
public class OrderDetailsDto
{
  public DateTimeOffset DateCreated { get; set; }
  public Guid OrderId { get; set; }
  public Guid UserId { get; set; }
  public List<OrderItemDetails> OrderItems { get; set; } = [];
}
