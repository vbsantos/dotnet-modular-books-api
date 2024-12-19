using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.EmailSending.Contracts;

namespace Vini.ModularMonolith.Example.EmailSending.Integrations;

internal class QueueEmailInOutboxSendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<Guid>>
{
  private readonly IOutboxService _outboxService;

  public QueueEmailInOutboxSendEmailCommandHandler(IOutboxService outboxService)
  {
    _outboxService = outboxService;
  }

  public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken ct)
  {
    var newEntity = new EmailOutboxEntity
    {
      To = request.To,
      From = request.From,
      Subject = request.Subject,
      Body = request.Body
    };

    await _outboxService.QueueEmailForSending(newEntity);

    return newEntity.Id;
  }
}
