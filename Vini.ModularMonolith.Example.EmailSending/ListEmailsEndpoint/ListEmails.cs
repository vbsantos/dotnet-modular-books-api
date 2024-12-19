using FastEndpoints;
using MongoDB.Driver;

namespace Vini.ModularMonolith.Example.EmailSending.ListEmailsEndpoint;

internal class ListEmails : EndpointWithoutRequest<ListEmailsResponse>
{
  private readonly IMongoCollection<EmailOutboxEntity> _emailCollection;

  // TODO: Normally we would create a use case for this, but for the
  // sake of simplicity we will just inject the mongodb collection here
  public ListEmails(IMongoCollection<EmailOutboxEntity> emailCollection)
  {
    _emailCollection = emailCollection;
  }

  public override void Configure()
  {
    Get("/emails");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    // TODO: Implement pagination
    var filter = Builders<EmailOutboxEntity>.Filter.Empty;
    var emailEntities = await _emailCollection.Find(filter).ToListAsync(ct);

    var response = new ListEmailsResponse
    {
      Count = emailEntities.Count,
      Emails = emailEntities // TODO: Use a separate DTO
    };

    Response = response;
  }
}
