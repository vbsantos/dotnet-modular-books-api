namespace Vini.ModularMonolith.Example.Reporting;

internal interface ITopSellingBooksReportService
{
  TopBooksByMonthReport ReachInSqlQuery(int month, int year);
}
