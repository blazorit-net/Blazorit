using Blazorit.Server.Services.Abstract.ECommerce.Domain.Orders;

namespace Blazorit.Server.Services.Concrete.ECommerce.Domain.Orders
{
    public class OrderService : IOrderService
    {

        private readonly Core.Services.Abstract.ECommerce.Domain.Orders.IOrderService _orderService;

        public OrderService(Core.Services.Abstract.ECommerce.Domain.Orders.IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<bool> CreateOrderFromCart(long userId)
        {
            return false; // await _orderService.CreateOrderFromCart(userId);
        }
    }
}
