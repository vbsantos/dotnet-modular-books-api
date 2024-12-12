using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Vini.ModularMonolith.Example.Users.Domain;

namespace Vini.ModularMonolith.Example.Users.UserEndpoints;

internal class Create : Endpoint<CreateUserRequest>
{
  private readonly UserManager<ApplicationUser> _userManager;

  public Create(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }

  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
  {
    var newUser = new ApplicationUser
    {
      Email = req.Email,
      UserName = req.Email
    };

    await _userManager.CreateAsync(newUser, req.Password);

    await SendOkAsync(ct);
  }
}
