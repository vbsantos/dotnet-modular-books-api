using System.Globalization;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Vini.ModularMonolith.Example.Reporting;

internal class DefaultSalesReportService : ISalesReportService
{
  private readonly ILogger<DefaultSalesReportService> _logger;
  private readonly string _connString;

  public DefaultSalesReportService(IConfiguration config, ILogger<DefaultSalesReportService> logger)
  {
    _connString = config.GetConnectionString("ReportingConnectionString")!;
    _logger = logger;
  }

  public async Task<TopBooksByMonthReport> GetTopBooksByMonthReportAsync(int month, int year)
  {
    var sql = @"
      SELECT BookId, Title, Author, Year, Month, UnitsSold AS Units, TotalSales AS Sales
      FROM Reporting.MonthlyBookSales
      WHERE Month = @Month AND Year = @Year
      ORDER BY TotalSales DESC
    ";

    using var conn = new SqlConnection(_connString);
    _logger.LogInformation("Executing query: {sql}", sql);

    var result = (await conn.QueryAsync<BookSalesResult>(sql, new { month, year })).ToList();

    var report = new TopBooksByMonthReport
    {
      Year = year,
      Month = month,
      MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
      Result = result
    };

    return report;
  }
}
