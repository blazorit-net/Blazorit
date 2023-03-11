using Blazorit.Core.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Core.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Core.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
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
        private readonly ICartService _cartService;
        private readonly IDeliveryService _deliveryService;

        public OrderService(IECommerceRepository dataRepo, ICartService cartService, IDeliveryService deliveryService)
        {
            _dataRepo = dataRepo;
            _cartService = cartService;
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
        public async Task<(bool ok, string paymentToken)> CreateUniqPaymentTokenAsync(decimal paymentAmount, long userId, long deliveryMethodId, long deliveryAddressId)
        {
            string paymentToken = Guid.NewGuid().ToString(); // TODO: implement creating uniq key (token)
            bool result = await _dataRepo.CreateUniqPaymentTokenAsync(paymentToken, paymentAmount, userId, deliveryMethodId, deliveryAddressId);

            if (result)
            {
                return (true, paymentToken);
            }

            return (false, string.Empty);
        }


        public async Task<(bool ok, long paymentId)> CreatePayment(long userId, string paymentInfo, decimal paymentAmount)
        {
           return await _dataRepo.CreatePaymentInfoAsync(paymentAmount, paymentInfo); // create info about payment
        }


        public async Task<bool> CreateOrderFromCart(long userId, long paymentId, long deliveryMethodId, long deliveryAddressId)
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
