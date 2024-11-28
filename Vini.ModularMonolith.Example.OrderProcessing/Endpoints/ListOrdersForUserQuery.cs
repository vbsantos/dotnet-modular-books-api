using Ardalis.Result;
using MediatR;

namespace Vini.ModularMonolith.Example.OrderProcessing.Endpoints;

internal record ListOrdersForUserQuery(string EmailAddress) : IRequest<Result<List<OrderSummary>>>;
