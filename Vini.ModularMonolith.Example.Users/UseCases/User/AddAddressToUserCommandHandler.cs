using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Vini.ModularMonolith.Example.Users.UseCases.User;
internal class AddAddressToUserCommandHandler : IRequestHandler<AddAddressToUserCommand, Result>
{
  private readonly IApplicationUserRepository _userRepository;
  private readonly ILogger<AddAddressToUserCommandHandler> _logger;

  public AddAddressToUserCommandHandler(IApplicationUserRepository userRepository, ILogger<AddAddressToUserCommandHandler> logger)
  {
    _userRepository = userRepository;
    _logger = logger;
  }

  public async Task<Result> Handle(AddAddressToUserCommand request, CancellationToken cancellationToken)
  {
    var user = await _userRepository.GetUserWithAddressesByEmailAsync(request.EmailAddress);

    if (user is null)
    {
      return Result.Unauthorized();
    }

    var addressToAdd = new Address(
      request.Street1,
      request.Street2,
      request.City,
      request.State,
      request.PostalCode,
      request.Country);

    var userAddress = user.AddAddress(addressToAdd);
    await _userRepository.SaveChangesAsync();

    _logger.LogInformation("[UseCase] Added address {address} to user {email} (Total addresses: {count})", userAddress, user.Email, user.Addresses.Count);

    return Result.Success();
  }
}
