using Blazorit.Core.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Core.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Core.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Orders;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
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
            decimal deliveryCost = orderData.Delivery.DeliveryCost.TotalCost;

            var delivery = await _deliveryService.InitDeliveryAsync(userId, deliveryMethodId, deliveryAddressId, deliveryCost); // init delivery

            if (delivery.ok == false)
            {
                return (false, string.Empty);
            }

            string paymentToken = Guid.NewGuid().ToString(); // TODO: implement creating uniq key (token)

            bool result = await _dataRepo.CreateUniqOrderTokenAsync(paymentToken, orderAmount, userId, delivery.deliveryId); // save to storage

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
        public async Task<Order?> CreateOrder(PaidOrder paidOrder)
        {
            long userId = paidOrder.UserId;
            string orderToken = paidOrder.OrderToken;
            decimal paidAmount = paidOrder.PaidAmount;
            string paymentInfo = paidOrder.PaymentInfo;

            CheckoutOrder? orderTokenData = await _dataRepo.GetTokenOrderInfoAsync(orderToken, userId); // get order data from storage

            if (orderTokenData == null)
            {
                return null;
            }

            // set payment info to storage
            var payment = await _dataRepo.CreatePaymentInfoAsync(paidAmount, orderTokenData.Id, orderToken, paymentInfo);

            if (!payment.ok)
            {
                return null;
            }


            //// if (paymentAmount < orderTokenData.OrderAmount) // then do error about it or log info about it and skip

            // create full order in storage
            var orderResult = await _dataRepo.CreateOrderFromCart(userId, payment.paymentId, orderTokenData.DeliveryId, orderToken); // create order

            if (!orderResult.ok)
            {
                return null;
            }

            // after creating order we return this order from storage
            Order? order = await GetUserOrderAsync(userId, orderResult.orderId);            
            return order;
        }


        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Order?> GetUserOrderAsync(long userId, long orderId)
        {
            Delivery? delivery = await _deliveryService.GetDeliveryByOrder(userId, orderId);

            IEnumerable<VwOrder> repoResult = await _dataRepo.GetUserOrderListAsync(userId, orderId);
            IEnumerable<OrderItem> listItems = await GetOrderItemsFromOrdersAsync(repoResult);            
            
            return listItems.Count() == 0 ? null : new Order(listItems, delivery ?? new Delivery());
        }

        /// <summary>
        /// Method converts IEnumerable<VwOrder> to IEnumerable<OrderItem> and adds additional data from repository (picture's link parts)
        /// </summary>
        /// <param name="orderList"></param>
        /// <returns></returns>
        private async Task<IEnumerable<OrderItem>> GetOrderItemsFromOrdersAsync(IEnumerable<VwOrder> orderList)
        {

            IEnumerable<OrderItem> result = orderList.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                Category = x.Category.Trim(),
                ProductLinkPart = x.ProductLinkPart.Trim(),
                Name = x.Name,
                ProductPrice = x.ProductPrice,
                OrderPrice = x.OrderPrice,
                Sku = x.Sku,
                Quantity = x.Quantity,
                ////DateTimeCreated = x.DateTimeItemCreate
                 
            }).ToList();

            foreach (var item in result)
            {
                item.PicturesLinkParts = (await _dataRepo.GetProductPictureLinkPartsAsync(item.ProductId, "medium", "site")) // select images for shopcart item
                    .Select(x => new PictureLinkPart
                    {
                        LinkPart = x.LinkPart.Trim(),
                        OrderNum = x.OrderNum,
                        PicSize = x.PicSize
                    }).OrderBy(x => x.OrderNum)
                    .ToList();

                item.ProductPictureLinkPart = item.PicturesLinkParts.FirstOrDefault()?.LinkPart.Trim() ?? string.Empty;
            }

            return result;
        }
    }
}
