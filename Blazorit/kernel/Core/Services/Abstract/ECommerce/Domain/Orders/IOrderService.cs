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
        Task<(bool ok, string paymentToken)> CreateUniqPaymentTokenAsync(decimal paymentAmount, long userId, long deliveryMethodId, long deliveryAddressId);

        //Task<(bool ok, long paymentId)> CreatePayment(long userId, string paymentInfo, decimal paymentAmount);

        Task<bool> CreateOrderFromCart(long userId, long paymentId, long deliveryMethodId, long deliveryAddressId);
    }
}
