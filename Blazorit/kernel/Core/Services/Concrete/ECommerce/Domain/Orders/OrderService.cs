using Blazorit.Core.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Core.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Core.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IECommerceRepository _dataRepo;
        private readonly IDeliveryService _deliveryService;

        public OrderService(IECommerceRepository dataRepo, IDeliveryService deliveryService)
        {
            _dataRepo = dataRepo;
            _deliveryService = deliveryService;
        }


        /// <summary>
        /// Method creates uniq token and info about order
        /// </summary>
        /// <param name="paymentAmount"></param>
        /// <param name="userId"></param>
        /// <param name="deliveryMethodId"></param>
        /// <param name="deliveryAddressId"></param>
        /// <returns></returns>
        public async Task<(bool ok, string paymentToken)> CreateUniqOrderTokenAsync(long userId, CheckOrder orderData)
        {
            decimal paymentAmount = orderData.TotalPrice;
            long deliveryMethodId = orderData.Delivery.UserDelivery.MethodId;
            long deliveryAddressId = orderData.Delivery.UserDelivery.AddressId;

            string paymentToken = Guid.NewGuid().ToString(); // TODO: implement creating uniq key (token)
            bool result = await _dataRepo.CreateUniqOrderTokenAsync(paymentToken, paymentAmount, userId, deliveryMethodId, deliveryAddressId); // save to storage

            if (result)
            {
                return (true, paymentToken);
            }

            return (false, string.Empty);
        }


        /// <summary>
        /// Method creates order
        /// </summary>
        /// <param name="orderCreation"></param>
        /// <returns></returns>
        public async Task<bool> CreateOrder(OrderCreation orderCreation)
        {
            long userId = orderCreation.UserId;
            string orderToken = orderCreation.OrderToken;
            decimal paymentAmount = orderCreation.PaymentAmount;
            string paymentInfo = orderCreation.PaymentInfo;

            // set payment info to storage
            var payInfo = await _dataRepo.CreatePaymentInfoAsync(paymentAmount, paymentInfo);

            if (!payInfo.ok)
            {
                return false;
            }

            CheckoutOrder? orderTokenData = await _dataRepo.GetTokenOrderInfoAsync(orderToken, userId); // get order data from storage

            if (orderTokenData == null)
            {
                return false;
            }

            //// if (paymentAmount < orderTokenData.PaymentAmount) // then do error about it or log info about it and skip

            // create full order in storage
            var result = await CreateOrderFromCart(userId, payInfo.paymentId, orderTokenData.DeliveryMethodId, orderTokenData.DeliveryAddressId);
            // TODO: if order created, then token to set as canceled
            return result;
        }


        /// <summary>
        /// Method creates order from cart by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paymentId"></param>
        /// <param name="deliveryMethodId"></param>
        /// <param name="deliveryAddressId"></param>
        /// <returns></returns>
        private async Task<bool> CreateOrderFromCart(long userId, long paymentId, long deliveryMethodId, long deliveryAddressId)
        {
            UserDelivery? userDelivery = await _deliveryService.GetUserDeliveryPoint(userId, deliveryMethodId, deliveryAddressId);  
            
            if (userDelivery == null)
            {
                return false;
            }

            return await _dataRepo.CreateOrderFromCart(userId, paymentId, userDelivery.Id); // create order
        }
    }
}
