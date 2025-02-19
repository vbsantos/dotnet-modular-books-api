﻿using System.Security.Claims;
using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Vini.ModularMonolith.Example.Users.UseCases.Cart.ListItems;

namespace Vini.ModularMonolith.Example.Users.CartEndpoints;

internal class ListCartItems : EndpointWithoutRequest<CartResponse>
{
  private readonly IMediator _mediator;

  public ListCartItems(IMediator mediator)
  {
    _mediator = mediator;
  }

  public override void Configure()
  {
    Get("/cart");
    Claims("EmailAddress");
  }

  public override async Task HandleAsync(CancellationToken ct)
  {
    var emailAddress = User.FindFirstValue("EmailAddress");

    var query = new ListCartItemsQuery(emailAddress!);

    var result = await _mediator.Send(query);

    if (result.Status == ResultStatus.Unauthorized)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }

    var response = new CartResponse
    {
      cartItems = result.Value
    };
    await SendAsync(response, cancellation: ct);
  }
}
