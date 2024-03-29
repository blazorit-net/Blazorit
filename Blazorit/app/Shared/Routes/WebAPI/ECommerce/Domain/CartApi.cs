﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Shared.Routes.WebAPI.ECommerce.Domain {
    public static class CartApi {
        public const string CONTROLLER = "api/ecommerce/domain/cart";
        public const string ADD_ITEM = "add-item";
        public const string DELETE_PRODUCT_ITEM = "delete-product-item";
        public const string GET_SHOPCART = "get-shopcart";
        public const string MERGE_SHOPCARTS = "merge-shopcarts";
    }
}
