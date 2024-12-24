namespace Vini.ModularMonolith.Example.Reporting;

internal class TopBooksByMonthReport
{
  public int Year { get; set; }
  public int Month { get; set; }
  public string MonthName { get; set; } = string.Empty;
  public List<BookSalesResult> Result { get; set; } = [];
}
