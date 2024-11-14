namespace Vini.ModularMonolith.Example.Books.BookEnpoints;

public class UpdateBookPriceRequest
{
  public Guid Id { get; set; }
  public decimal NewPrice { get; set; }
}
