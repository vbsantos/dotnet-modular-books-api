using FastEndpoints;

namespace Vini.ModularMonolith.Example.Reporting.ReportEndpoints;

internal class TopSalesByMonth2 : Endpoint<TopSalesByMonthRequest, TopSalesByMonthResponse>
{
  private readonly ISalesReportService _salesReportService;

  public TopSalesByMonth2(ISalesReportService salesReportService)
  {
    _salesReportService = salesReportService;
  }

  public override void Configure()
  {
    Get("topSales2");
    AllowAnonymous(); // TODO: Lockdown
  }

  public override async Task HandleAsync(TopSalesByMonthRequest request, CancellationToken ct = default)
  {
    var report = await _salesReportService.GetTopBooksByMonthReportAsync(request.Month, request.Year);

    var response = new TopSalesByMonthResponse { Report = report };

    await SendAsync(response, cancellation: ct);
  }
}
