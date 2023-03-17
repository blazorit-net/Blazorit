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
        /// <param name="userId"></param>
        /// <param name="orderData"></param>
        /// <returns></returns>
        public async Task<(bool ok, string paymentToken)> CreateUniqOrderTokenAsync(long userId, CheckOrder orderData)
        {
            decimal orderAmount = orderData.TotalPrice;
            long deliveryMethodId = orderData.Delivery.UserDelivery.MethodId;
            long deliveryAddressId = orderData.Delivery.UserDelivery.AddressId;

            UserDelivery? userDelivery = await _deliveryService.InitUserDeliveryAsync(userId, deliveryMethodId, deliveryAddressId); // init user delivery point

            if (userDelivery == null)
            {
                return (false, string.Empty);
            }

            string paymentToken = Guid.NewGuid().ToString(); // TODO: implement creating uniq key (token)

            bool result = await _dataRepo.CreateUniqOrderTokenAsync(paymentToken, orderAmount, userId, userDelivery.Id); // save to storage

            if (result)
            {
                return (true, paymentToken);
            }

            return (false, string.Empty);
        }


        /// <summary>
        /// Method creates order
        /// </summary>
        /// <param name="paidOrder"></param>
        /// <returns></returns>
        public async Task<bool> CreateOrder(PaidOrder paidOrder)
        {
            long userId = paidOrder.UserId;
            string orderToken = paidOrder.OrderToken;
            decimal paidAmount = paidOrder.PaidAmount;
            string paymentInfo = paidOrder.PaymentInfo;

            CheckoutOrder? orderTokenData = await _dataRepo.GetTokenOrderInfoAsync(orderToken, userId); // get order data from storage

            if (orderTokenData == null)
            {
                return false;
            }

            // set payment info to storage
            var payment = await _dataRepo.CreatePaymentInfoAsync(paidAmount, orderTokenData.Id, orderToken, paymentInfo);

            if (!payment.ok)
            {
                return false;
            }


            //// if (paymentAmount < orderTokenData.OrderAmount) // then do error about it or log info about it and skip

            // create full order in storage
            var result = await _dataRepo.CreateOrderFromCart(userId, payment.paymentId, orderTokenData.UserDeliveryId, orderToken); // create order
            // TODO: if order created, then token to set as canceled
            return result;
        }
    }
}
