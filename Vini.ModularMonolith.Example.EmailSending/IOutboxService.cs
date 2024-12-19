using Ardalis.Result;

namespace Vini.ModularMonolith.Example.EmailSending;

internal interface IOutboxService
{
  Task QueueEmailForSending(EmailOutboxEntity entity);
  Task<Result<EmailOutboxEntity>> GetUnprocessedEmailEntity();
}
