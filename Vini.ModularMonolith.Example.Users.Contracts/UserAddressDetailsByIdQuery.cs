using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.Users.Contracts;

public record UserAddressDetailsByIdQuery(Guid AddressId) : IRequest<Result<UserAddressDetails>>;
