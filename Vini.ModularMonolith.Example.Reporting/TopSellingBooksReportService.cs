using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Vini.ModularMonolith.Example.Reporting;

internal class TopSellingBooksReportService : ITopSellingBooksReportService
{
  private readonly ILogger<TopSellingBooksReportService> _logger;
  private readonly string _connString;

  public TopSellingBooksReportService(
    IConfiguration config,
    ILogger<TopSellingBooksReportService> logger)
  {
    _connString = config.GetConnectionString("OrderProcessingConnectionString")!;
    _logger = logger;
  }

  public TopBooksByMonthReport ReachInSqlQuery(int month, int year)
  {
    string sql = @"
      SELECT b.Id AS BookId, b.Title, b.Author, sum(oi.Quantity) AS Units, sum(oi.UnitPrice * oi.Quantity) as Sales
      FROM Books.Books b
        INNER JOIN OrderProcessing.OrderItem oi ON b.Id = oi.BookId
        INNER JOIN OrderProcessing.Orders o ON o.Id = oi.OrderId
      WHERE MONTH(o.DateCreated) = @month AND YEAR(o.DateCreated) = @year
      GROUP BY b.Id, b.Title, b.Author
      ORDER BY Sales DESC
    ";

    using var conn = new SqlConnection(_connString);
    _logger.LogInformation("Executing query: {sql}", sql);
    var results = conn.Query<BookSalesResult>(sql, new { month, year })
      .ToList();

    var report = new TopBooksByMonthReport
    {
      Year = year,
      Month = month,
      MonthName = CultureInfo.GetCultureInfo("en-US").DateTimeFormat.GetMonthName(month),
      Result = results
    };

    return report;
  }
}
