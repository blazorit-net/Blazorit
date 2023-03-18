﻿using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders
{
    public class Order
    {
        private List<OrderItem> orderList = new List<OrderItem>();

        public Order() { }

        public Order(IEnumerable<OrderItem> cartList)
        {
            OrderList = cartList.ToList();
        }

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
    }
}