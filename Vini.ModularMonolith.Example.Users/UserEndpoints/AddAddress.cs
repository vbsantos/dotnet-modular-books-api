using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Vini.ModularMonolith.Example.Users.UseCases.User.AddAddress;

namespace Vini.ModularMonolith.Example.Users.UserEndpoints;

internal class AddAddress : Endpoint<AddAddressRequest>
{
  private readonly IMediator _mediator;

  public AddAddress(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Post("/users/addresses");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(AddAddressRequest req, CancellationToken ct)
  {
    var emailAddress = User.FindFirstValue("EmailAddress");

    var command = new AddAddressToUserCommand(emailAddress!, req.Street1, req.Street2, req.City, req.State, req.PostalCode, req.Country);

    var result = await _mediator.Send(command, ct);

    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
    }
    else
    {
      await SendOkAsync(ct);
    }
  }
}
