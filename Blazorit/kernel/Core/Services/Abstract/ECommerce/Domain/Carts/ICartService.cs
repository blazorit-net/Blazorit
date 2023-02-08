using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.ECommerce.Domain.Carts {
    /// <summary>
    /// Cart service for shopcarts
    /// </summary>
    public interface ICartService {
        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ShopCart?> GetShopCartListAsync(long userId);

        /// <summary>
        /// Method adds product (quantity of product) to shopcart by SKU
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>shopcart list</returns>
        Task<ShopCart?> AddProductToCartAsync(long userId, string productSKU, int quantity);

        /// <summary>
        /// Method adds product (quantity of product) to shopcart by productId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<ShopCart?> AddProductToCartAsync(long userId, long productId, int quantity);

        /// <summary>
        /// Method merges client shopcart with kernel cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientCart"></param>
        /// <returns>Result cart</returns>
        Task<ShopCart?> MergeShopCarts(long userId, ShopCart clientCart);
    }
}
