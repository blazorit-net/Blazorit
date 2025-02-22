using Blazorit.Shared.Models.Universal;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;

namespace Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders
{
    public interface IOrderService
    {
        /// <summary>
        /// Method creates uniq token and info about order
        /// </summary>
        /// <param name="orderData"></param>
        /// <returns></returns>
        Task<Response<string>> CreateUniqOrderTokenAsync(CheckOrder orderData);

        /// <summary>
        /// Method creates order
        /// </summary>
        /// <param name="orderCreation"></param>
        /// <returns></returns>
        Task<Response<Order>> CreateOrder(PaidOrder orderCreation);
    }
}
