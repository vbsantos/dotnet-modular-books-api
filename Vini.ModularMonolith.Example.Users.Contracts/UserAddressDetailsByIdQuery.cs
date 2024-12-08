using Ardalis.Result;
using MediatR;
using Users.Contracts;

namespace Vini.ModularMonolith.Example.Users.Contracts;

public record UserAddressDetailsByIdQuery(Guid AddressId) : IRequest<Result<UserAddressDetails>>;
