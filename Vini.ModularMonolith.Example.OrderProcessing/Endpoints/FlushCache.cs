using FastEndpoints;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Vini.ModularMonolith.Example.OrderProcessing.Endpoints;

internal class FlushCache : EndpointWithoutRequest
{
  private readonly IDatabase _db;
  private readonly ILogger<FlushCache> _logger;

  public FlushCache(ILogger<FlushCache> logger)
  {
    var redis = ConnectionMultiplexer.Connect("localhost");
    _db = redis.GetDatabase();
    _logger = logger;
  }

  public override void Configure()
  {
    Post("/flushcache");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct = default)
  {
    await _db.ExecuteAsync("FLUSHDB");
    _logger.LogInformation("FLUSHED CACHE FOR {db}", "REDIS");
  }
}
