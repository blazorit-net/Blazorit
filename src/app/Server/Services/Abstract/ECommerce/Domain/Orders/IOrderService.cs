using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;

namespace Blazorit.Server.Services.Abstract.ECommerce.Domain.Orders
{
    public interface IOrderService
    {
        /// <summary>
        /// Method creates uniq token and info about order 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orderData"></param>
        /// <returns></returns>
        Task<(bool ok, string paymentToken)> CreateUniqOrderTokenAsync(long userId, CheckOrder orderData);

        /// <summary>
        /// Method creates order
        /// </summary>
        /// <param name="orderCreation"></param>
        /// <returns></returns>
        Task<Order?> CreateOrder(PaidOrder orderCreation);
    }
}
