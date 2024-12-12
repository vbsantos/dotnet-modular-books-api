using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.Users.Contracts;
using Vini.ModularMonolith.Example.Users.Interfaces;

namespace Vini.ModularMonolith.Example.Users.Integrations;

public class UserAddressDetailsByIdQueryHandler : IRequestHandler<UserAddressDetailsByIdQuery, Result<UserAddressDetails>>
{
  private readonly IReadOnlyUserStreetAddressRepository _addressRepo;

  public UserAddressDetailsByIdQueryHandler(IReadOnlyUserStreetAddressRepository addressRepo)
  {
    _addressRepo = addressRepo;
  }

  public async Task<Result<UserAddressDetails>> Handle(UserAddressDetailsByIdQuery request, CancellationToken cancellationToken)
  {
    var address = await _addressRepo.GetById(request.AddressId);

    if (address is null)
    {
      return Result.NotFound();
    }

    Guid userId = Guid.Parse(address.UserId);

    var details = new UserAddressDetails(userId,
      address.Id,
      address.StreetAddress.Street1,
      address.StreetAddress.Street2,
      address.StreetAddress.City,
      address.StreetAddress.State,
      address.StreetAddress.PostalCode,
      address.StreetAddress.Country);

    return details;
  }
}
