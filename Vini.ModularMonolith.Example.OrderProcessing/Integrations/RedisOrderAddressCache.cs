using System.Text.Json;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Vini.ModularMonolith.Example.OrderProcessing.Integrations;

internal class RedisOrderAddressCache : IOrderAddressCache
{
  private readonly IDatabase _db;
  private readonly ILogger<RedisOrderAddressCache> _logger;

  public RedisOrderAddressCache(ILogger<RedisOrderAddressCache> logger)
  {
    var redis = ConnectionMultiplexer.Connect("localhost");
    _db = redis.GetDatabase();
    _logger = logger;
  }

  public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
  {
    string? fetchedJson = await _db.StringGetAsync(addressId.ToString());
    if (fetchedJson is null)
    {
      _logger.LogWarning("Address with ID {id} not found in {db}", addressId, "REDIS");
      return Result<OrderAddress>.NotFound();
    }

    var address = JsonSerializer.Deserialize<OrderAddress>(fetchedJson);
    if (address is null)
    {
      return Result.NotFound();
    }

    _logger.LogInformation("Address with ID {id} found in {db}", addressId, "REDIS");
    return Result.Success(address);
  }

  public async Task<Result> StoreAsync(OrderAddress orderAddress)
  {
    var key = orderAddress.Id.ToString();
    var addressJson = JsonSerializer.Serialize(orderAddress);

    await _db.StringSetAsync(key, addressJson);
    _logger.LogInformation("Address with ID {id} stored in {db}", orderAddress.Id, "REDIS");

    return Result.Success();
  }
}
