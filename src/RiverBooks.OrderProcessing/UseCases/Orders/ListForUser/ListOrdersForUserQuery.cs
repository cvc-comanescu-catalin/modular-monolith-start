using Ardalis.Result;
using MediatR;

namespace RiverBooks.OrderProcessing.UseCases.Orders.ListForUser;

internal record ListOrdersForUserQuery(string EmailAddress) : IRequest<Result<List<OrderSummary>>>;
