using Ardalis.Result;
using Vini.ModularMonolith.Example.EmailSending.Contracts;
using Vini.ModularMonolith.Example.EmailSending.EmailBackgroundService;

namespace Vini.ModularMonolith.Example.EmailSending.Integrations;

internal class SendEmailCommandHandler //: IRequestHandler<SendEmailCommand, Result<Guid>>
{
  private readonly ISendEmail _emailSender;

  public SendEmailCommandHandler(ISendEmail emailSender)
  {
    _emailSender = emailSender;
  }

  public async Task<Result<Guid>> HandleAsync(SendEmailCommand request, CancellationToken ct)
  {
    await _emailSender.SendEmailAsync(request.To, request.From, request.Subject, request.Body);

    return Guid.Empty;
  }
}
