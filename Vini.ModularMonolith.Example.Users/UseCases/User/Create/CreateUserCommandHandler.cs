using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Vini.ModularMonolith.Example.EmailSending.Contracts;
using Vini.ModularMonolith.Example.Users.Domain;

namespace Vini.ModularMonolith.Example.Users.UseCases.User.Create;

internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IMediator _mediator;

  public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMediator mediator)
  {
    _userManager = userManager;
    _mediator = mediator;
  }

  public async Task<Result> Handle(CreateUserCommand command, CancellationToken ct)
  {
    var newUser = new ApplicationUser
    {
      Email = command.Email,
      UserName = command.Email
    };

    var result = await _userManager.CreateAsync(newUser, command.Password);

    if (!result.Succeeded)
    {
      return Result.Error(new ErrorList(result.Errors.Select(e => e.Description).ToArray()));
    }

    // Send welcome email
    var sendEmailCommand = new SendEmailCommand
    {
      To = newUser.Email,
      From = "donotreply@test.com",
      Subject = "Welcome to ModularMonolith Books!",
      Body = "Welcome to ModularMonolith Books! Thank you for registering."
    };

    await _mediator.Send(sendEmailCommand, ct);

    return Result.Success();
  }
}
