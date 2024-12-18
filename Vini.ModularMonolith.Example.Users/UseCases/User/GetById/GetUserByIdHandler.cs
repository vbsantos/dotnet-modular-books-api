using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.Users.Interfaces;

namespace Vini.ModularMonolith.Example.Users.UseCases.User.GetById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
{
  private readonly IApplicationUserRepository _userRepository;

  public GetUserByIdHandler(IApplicationUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken ct)
  {
    var user = await _userRepository.GetUserByIdAsync(request.UserId);

    if (user is null)
    {
      return Result.NotFound();
    }

    return new UserDTO(Guid.Parse(user.Id), user.Email!);
  }
}
