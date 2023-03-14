using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.ECommerce.Domain.Orders
{
    public interface IOrderService
    {
        /// <summary>
        /// Method creates uniq token and info about order
        /// </summary>
        /// <param name="paymentAmount"></param>
        /// <param name="userId"></param>
        /// <param name="deliveryMethodId"></param>
        /// <param name="deliveryAddressId"></param>
        /// <returns></returns>
        Task<(bool ok, string paymentToken)> CreateUniqOrderTokenAsync(long userId, CheckOrder orderData);

        /// <summary>
        /// Method creates order
        /// </summary>
        /// <param name="orderCreation"></param>
        /// <returns></returns>
        Task<bool> CreateOrder(OrderCreation orderCreation);

        //Task<bool> CreateOrderFromCart(long userId, long paymentId, long deliveryMethodId, long deliveryAddressId);
    }
}
