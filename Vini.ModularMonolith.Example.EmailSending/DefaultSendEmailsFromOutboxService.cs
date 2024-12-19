using Ardalis.Result;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Vini.ModularMonolith.Example.EmailSending;

internal class DefaultSendEmailsFromOutboxService : ISendEmailsFromOutboxService
{
  private readonly IOutboxService _outboxService;
  private readonly ISendEmail _emailSender;
  // TODO: If it was production code it would probably be behind a repository instead
  private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;
  private readonly ILogger<DefaultSendEmailsFromOutboxService> _logger;

  public DefaultSendEmailsFromOutboxService(
    IOutboxService outboxService,
    ISendEmail emailSender,
    IMongoCollection<EmailOutboxEntity> emailCollection,
    ILogger<DefaultSendEmailsFromOutboxService> logger)
  {
    _outboxService = outboxService;
    _emailSender = emailSender;
    _emailCollection = emailCollection;
    _logger = logger;
  }

  public async Task CheckForAndSendEmails()
  {
    try
    {
      var result = await _outboxService.GetUnprocessedEmailEntity();

      if (result.Status == ResultStatus.NotFound)
      {
        return;
      }

      var emailEntity = result.Value;

      await _emailSender.SendEmailAsync(
        emailEntity.To,
        emailEntity.From,
        emailEntity.Subject,
        emailEntity.Body);

      var updateFilter = Builders<EmailOutboxEntity>.Filter
        .Eq(e => e.Id, emailEntity.Id);

      var updated = Builders<EmailOutboxEntity>.Update
        .Set(e => e.DateTimeUtcProcessed, DateTime.UtcNow);

      var updateResult = await _emailCollection
        .UpdateOneAsync(updateFilter, updated);

      _logger.LogInformation("Processed {result} email records.", updateResult.ModifiedCount);
    }
    finally
    {
      _logger.LogInformation("Sleeping.");
    }
  }
}
