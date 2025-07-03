using Ardalis.Result;
using MediatR;
using Microsoft.Extensions.Logging;
using RiverBooks.OrderProcessing.Contracts;

namespace RiverBooks.OrderProcessing.Integrations;

internal class CreateOrderCommandHandler :
  IRequestHandler<CreateOrderCommand, Result<OrderDetailsResponse>>
{
  private readonly IOrderRepository _orderRepository;

  private readonly ILogger<CreateOrderCommandHandler> _logger;

  private readonly IOrderAddressCache _addressCache;

  public CreateOrderCommandHandler(IOrderRepository orderRepository,
    ILogger<CreateOrderCommandHandler> logger,
    IOrderAddressCache addressCache)
  {
    _orderRepository = orderRepository;
    _logger = logger;
    _addressCache = addressCache;
  }

  public async Task<Result<OrderDetailsResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    // need shipping and billing address info - how can we get it?
    // Option 1: Pass it into the command with everything else
    // Option 2: Pass in the IDs, look them up from the Users module's database
    // Option 3: Pass in the IDs, look them up via a Users module Query
    // Option 4: Pass in the IDs, look them up via a Users module HTTP API call
    // Option 5: Pass in the IDs, look them up in our local cache (materialized view)

    // Analysis:
    // Option 1 is a good one and a good default to use. Pass whatever is needed with the message/command.
    // Option 2 is bad; breaks modularity and adds tight coupling.
    // Option 3 is OK and is a good default to use with modular monoliths
    // Option 4 isn't ideal but is a common approach in microservices (where it adds temporal coupling)
    // Option 5 is ideal in microservices, less common in MM architecture but let's see what it looks like

    // need to look up user addresses in materialized view
    // IMPORTANT: addresses are immutable - if we have the id we know we have the correct address
    // var billingAddress = await _addressCache.GetByIdAsync(command.BillingAddressId);
    // var shippingAddress = await _addressCache.GetByIdAsync(command.ShippingAddressId);
    // TODO: Implement read-through and retry logic via decorator(s)

    // no need to look up book details; description on items will work

    // business rule: we create the order exactly as the cart shows (using its price, description, etc.)
    var orderItems = request.OrderItems.Select(oi => new OrderItem(
      oi.BookId, oi.Quantity, oi.UnitPrice, oi.Description));

    var shippingAddress = await _addressCache.GetByIdAsync(request.ShippingAddressId);
    var billingAddress = await _addressCache.GetByIdAsync(request.BillingAddressId);

    var newOrder = Order.Factory.Create(request.UserId,
                                     billingAddress.Value.Address,
                                     shippingAddress.Value.Address,
                                     orderItems);

    await _orderRepository.AddAsync(newOrder);
    await _orderRepository.SaveChangesAsync();

    _logger.LogInformation("New Order Created! {orderId}", newOrder.Id);

    return new OrderDetailsResponse(newOrder.Id);
  }
}
