using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace Vini.ModularMonolith.Example.Users.UserEndpoints;

internal class Login : Endpoint<UserLoginRequest>
{
  private readonly UserManager<ApplicationUser> _userManager;

  public Login(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }

  public override void Configure()
  {
    Post("/users/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UserLoginRequest req, CancellationToken ct)
  {
    var user = await _userManager.FindByEmailAsync(req.Email);
    if (user is null)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }

    var loginSuccessful = await _userManager.CheckPasswordAsync(user, req.Password);
    if (!loginSuccessful)
    {
      await SendUnauthorizedAsync(ct);
      return;
    }

    var token = JwtBearer.CreateToken(o =>
    {
      o.SigningKey = Config["Auth:JwtSecret"]!;
      o.User.Claims.Add(("EmailAddress", user.Email!));
    });

    await SendAsync(token, cancellation: ct);
  }
}
