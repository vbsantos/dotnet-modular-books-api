namespace Vini.ModularMonolith.Example.Reporting;

internal interface ISalesReportService
{
  Task<TopBooksByMonthReport> GetTopBooksByMonthReportAsync(int month, int year);
}
