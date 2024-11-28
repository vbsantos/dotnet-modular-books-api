using Ardalis.Result;
using MediatR;
using Vini.ModularMonolith.Example.Users.UserEndpoints;

namespace Vini.ModularMonolith.Example.Users.UseCases.User;

internal record ListAddressesQuery(string EmailAddress) : IRequest<Result<List<UserAddressDto>>>;
