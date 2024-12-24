using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Vini.ModularMonolith.Example.Reporting.ReportEndpoints;

internal class TopSalesByMonthRequest
{
  [FromQuery]
  public int Year { get; set; }
  [FromQuery]
  public int Month { get; set; }
}

internal class TopSalesByMonthResponse
{
  public TopBooksByMonthReport Report { get; set; } = default!;
}

internal class TopSalesByMonth : Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
  private readonly ITopSellingBooksReportService _topSellingBooksReportService;

  public TopSalesByMonth(ITopSellingBooksReportService topSellingBooksReportService)
  {
    _topSellingBooksReportService = topSellingBooksReportService;
  }

  public override void Configure()
  {
    Get("topSales");
    AllowAnonymous(); // TODO: Lockdown
  }

  public override async Task HandleAsync(TopSalesByMonthRequest request, CancellationToken ct = default)
  {
    var report = _topSellingBooksReportService.ReachInSqlQuery(request.Month, request.Year);

    var response = new TopSalesByMonthResponse { Report = report };

    await SendAsync(response, cancellation: ct);
  }
}
