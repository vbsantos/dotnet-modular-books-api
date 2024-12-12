using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using Vini.ModularMonolith.Example.OrderProcessing.Domain;
using Vini.ModularMonolith.Example.OrderProcessing.Infrastructure.Data;
using Vini.ModularMonolith.Example.OrderProcessing.Interfaces;

namespace Vini.ModularMonolith.Example.OrderProcessing.Infrastructure;

internal class ReadThroughOrderAddressCache : IOrderAddressCache
{
  private readonly RedisOrderAddressCache _redisCache;
  private readonly IMediator _mediator;
  private readonly ILogger<ReadThroughOrderAddressCache> _logger;

  public ReadThroughOrderAddressCache(
    ILogger<ReadThroughOrderAddressCache> logger,
    RedisOrderAddressCache redisCache,
    IMediator mediator)
  {
    _logger = logger;
    _redisCache = redisCache;
    _mediator = mediator;
  }

  public async Task<Result<OrderAddress>> GetByIdAsync(Guid addressId)
  {
    var result = await _redisCache.GetByIdAsync(addressId);
    if (result.IsSuccess)
    {
      return result;
    }

    if (result.Status == ResultStatus.NotFound)
    {
      _logger.LogInformation("Address {id} not found; fetching from source.", addressId);
      var query = new Users.Contracts.UserAddressDetailsByIdQuery(addressId);

      var queryResult = await _mediator.Send(query);

      if (queryResult.IsSuccess)
      {
        var dto = queryResult.Value;
        var address = new Address(
          dto.Street1,
          dto.Street2,
          dto.City,
          dto.State,
          dto.PostalCode,
          dto.Country);
        var orderAddress = new OrderAddress(dto.AddressId, address);
        await StoreAsync(orderAddress);
        return orderAddress;
      }

    }
    return Result<OrderAddress>.NotFound();
  }

  public Task<Result> StoreAsync(OrderAddress orderAddress)
  {
    return _redisCache.StoreAsync(orderAddress);
  }
}
