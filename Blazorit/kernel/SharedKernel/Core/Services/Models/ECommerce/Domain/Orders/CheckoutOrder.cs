using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders
{
    /// <summary>
    /// This class must contain all info (total price, qty products, delivery cost, payment method) for creating an order
    /// </summary>
    public class CheckoutOrder
    {
        public CheckoutOrder() { }

        //public CheckoutOrder(ShopCart shopCart, DeliveryCost deliveryCost)
        //{
        //    ShopCart = shopCart;
        //    DeliveryCost = deliveryCost;
        //}

        public CheckoutOrder(ShopCart shopCart, Delivery delivery)
        {
            ShopCart = shopCart;
            Delivery = delivery;
        }

        public ShopCart ShopCart { get; set; } = new();

        //public DeliveryCost DeliveryCost { get; set; } = new();

        public Delivery Delivery { get; set; } = new();


        /// <summary>
        /// Payment amount.
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                return SubTotalPrice + Delivery.DeliveryCost.TotalCost;
            }
        }

        /// <summary>
        /// Quantity, number of goods in shopcart 
        /// </summary>
        public int SubTotalQuantity
        {
            get
            {
                return ShopCart.TotalQuantity;
            }
        }

        /// <summary>
        /// Total price in shopcart
        /// </summary>
        public decimal SubTotalPrice
        {
            get
            {
                return ShopCart.TotalPrice;
            }
        }

        /// <summary>
        /// String type of: SubTotalQuantity
        /// </summary>
        public string StrSubTotalQuantity
        {
            get
            {
                return SubTotalQuantity.ToString("N0");
            }
        }

        /// <summary>
        /// String type of: SubTotalPrice
        /// </summary>
        public string StrSubTotalPrice
        {
            get
            {
                return SubTotalPrice.ToString("N0");
            }
        }

        /// <summary>
        /// String type of: TotalPrice (Payment amount)
        /// </summary>
        public string StrTotalPrice
        {
            get
            {
                return TotalPrice.ToString("N0");
            }
        }
    }
}
