using Ardalis.Result;
using MongoDB.Driver;

namespace Vini.ModularMonolith.Example.EmailSending;

internal class MongoDbOutboxService : IOutboxService
{
  private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

  public MongoDbOutboxService(IMongoCollection<EmailOutboxEntity> emailCollection)
  {
    _emailCollection = emailCollection;
  }

  public async Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity()
  {
    var filter = Builders<EmailOutboxEntity>.Filter
      .Eq(e => e.DateTimeUtcProcessed, null);

    var unsentEmailEntity = await _emailCollection.Find(filter)
      .FirstOrDefaultAsync();

    if (unsentEmailEntity is null)
    {
      return Result.NotFound();
    }

    return unsentEmailEntity;
  }

  public async Task QueueEmailForSending(EmailOutboxEntity entity)
  {
    await _emailCollection.InsertOneAsync(entity);
  }
}
