namespace Vini.ModularMonolith.Example.EmailSending;

public interface ISendEmailsFromOutboxService
{
  Task CheckForAndSendEmails();
}
