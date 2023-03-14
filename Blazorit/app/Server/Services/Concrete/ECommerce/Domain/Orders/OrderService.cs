using Blazorit.Server.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;

namespace Blazorit.Server.Services.Concrete.ECommerce.Domain.Orders
{
    public class OrderService : IOrderService
    {

        private readonly Core.Services.Abstract.ECommerce.Domain.Orders.IOrderService _orderService;

        public OrderService(Core.Services.Abstract.ECommerce.Domain.Orders.IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Method creates uniq token and info about order 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderData"></param>
        /// <returns></returns>
        public async Task<(bool ok, string paymentToken)> CreateUniqOrderTokenAsync(long userId, CheckOrder orderData)
        {
            var result = await _orderService.CreateUniqOrderTokenAsync(userId, orderData);
            return result;
        }


        /// <summary>
        /// Method creates order
        /// </summary>
        /// <param name="orderCreation"></param>
        /// <returns></returns>
        public async Task<bool> CreateOrder(OrderCreation orderCreation)
        {
            var result = await _orderService.CreateOrder(orderCreation);
            return result;
        }
    }
}
