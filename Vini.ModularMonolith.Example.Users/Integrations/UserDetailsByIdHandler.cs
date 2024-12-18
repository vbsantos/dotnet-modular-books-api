using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.Users.Contracts;
using Vini.ModularMonolith.Example.Users.UseCases.User.GetById;

namespace Vini.ModularMonolith.Example.Users.Integrations;

public class UserDetailsByIdHandler : IRequestHandler<UserDetailsByIdQuery, Result<UserDetails>>
{
  private readonly IMediator _mediator;

  public UserDetailsByIdHandler(IMediator mediator)
  {
    _mediator = mediator;
  }

  public async Task<Result<UserDetails>> Handle(UserDetailsByIdQuery request, CancellationToken ct)
  {
    var query = new GetUserByIdQuery(request.UserId);
    var result = await _mediator.Send(query, ct);
    var userDetails = result.Map(u => new UserDetails(u.UserId, u.EmailAddress));
    return userDetails;
  }
}
