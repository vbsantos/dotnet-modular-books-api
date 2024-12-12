using Vini.ModularMonolith.Example.SharedKernel;

namespace Vini.ModularMonolith.Example.Users.Contracts;

public record NewUserAddressAddedIntegrationEvent(UserAddressDetails Details)
  : IntegrationEventBase;
