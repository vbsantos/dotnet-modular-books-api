using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Vini.ModularMonolith.Example.Users.UseCases.User;

namespace Vini.ModularMonolith.Example.Users.UserEndpoints;

internal class ListAddresses : EndpointWithoutRequest<AddressListResponse>
{
  private readonly IMediator _mediator;

  public ListAddresses(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Get("/users/addresses");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var emailAddress = User.FindFirstValue("EmailAddress");

    var query = new ListAddressesQuery(emailAddress!);

    var result = await _mediator.Send(query, ct);

    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      var response = new AddressListResponse
      {
        Addresses = result.Value
      };
      await SendOkAsync(response, ct);
    }
  }
}
