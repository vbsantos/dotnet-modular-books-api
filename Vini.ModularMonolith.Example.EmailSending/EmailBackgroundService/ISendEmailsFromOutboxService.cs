namespace Vini.ModularMonolith.Example.EmailSending.EmailBackgroundService;

public interface ISendEmailsFromOutboxService
{
  Task CheckForAndSendEmailsAsync();
}
