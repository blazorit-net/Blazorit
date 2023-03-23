using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders
{
    public class Order
    {
        private List<OrderItem> orderList = new List<OrderItem>();

        public Order() { }

        public Order(long orderId, DateTimeOffset dateCreate, IEnumerable<OrderItem> cartList, Delivery delivery, Payment payment)
        {
            this.OrderId = orderId;
            this.DateCreate = dateCreate;
            this.OrderList = cartList.ToList();
            this.Delivery = delivery;
            this.Payment = payment;
        }


        public long OrderId { get; set; }

        public DateTimeOffset DateCreate { get; set; }

        public Payment Payment { get; set; } = new();


        public Delivery Delivery { get; set; } = new();


        public List<OrderItem> OrderList
        {
            get
            {
                orderList = orderList.OrderByDescending(x => x.Sku).ToList(); // ordering for all views
                return orderList;
            }
            set
            {
                orderList = value;
            }
        }

        public int TotalQuantity
        {
            get
            {
                return OrderList.Sum(x => x.Quantity);
            }
        }

        public decimal TotalProductPrice
        {
            get
            {
                return OrderList.Sum(x => x.TotalProductPrice);
            }
        }

        public string StrTotalQuantity
        {
            get
            {
                return TotalQuantity.ToString("N0");
            }
        }

        public string StrTotalProductPrice
        {
            get
            {
                return TotalProductPrice.ToString("N0");
            }
        }


        public decimal TotalOrderPrice
        {
            get
            {
                return OrderList.Sum(x => x.TotalOrderPrice);
            }
        }


        public string StrTotalOrderPrice
        {
            get
            {
                return TotalOrderPrice.ToString("N0");
            }
        }

        public string StrDateCreate
        {
            get
            {
                if (DateTimeOffset.Now.Year == DateCreate.Year)
                {
                    return DateCreate.ToLocalTime().ToString("dd MMMM, HH:mm");
                }

                return DateCreate.ToLocalTime().ToString("dd MMMM yyyy, HH:mm");
            }
        }
    }
}
