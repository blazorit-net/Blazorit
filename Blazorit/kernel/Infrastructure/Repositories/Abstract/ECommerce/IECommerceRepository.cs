using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Infrastructure.Repositories.Abstract.ECommerce {
    public interface IECommerceRepository {
        /// <summary>
        /// Method adds product to products repository. This method assigns unique SKU for the product
        /// </summary>
        /// <param name="name">Product name</param>
        /// <param name="curr">Currency</param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="categoryName"></param>
        /// <returns>(Success, unique SKU)</returns>
        Task<(bool ok, string sku)> AddProductAsync(string name, string curr, decimal price, string? description, string? categoryName);

        /// <summary>
        /// Method adds product to products repository. You need assign SKU for the product
        /// </summary>
        /// <param name="sku">SKU (Stock Keeping Unit) (articul)</param>
        /// <param name="name">Product name</param>
        /// <param name="curr">Currency</param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="categoryName"></param>
        /// <returns>(Success, unique SKU)</returns>
        Task<(bool ok, string sku)> AddProductAsync(string sku, string name, string curr, decimal price, string? description, string? categoryName);

        /// <summary>
        /// Method adds product to user's cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>(Success, cartId)</returns>
        Task<(bool ok, long cartId)> AddProductToCartAsync(long userId, string productSKU, int quantity);

        /// <summary>
        /// Method adds product to user's cart by cartId (this method is a bit faster than AddProductToCartAsync method)
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>Success</returns>
        Task<bool> AddProductToCartByCartIdAsync(long cartId, string productSKU, int quantity);

        /// <summary>
        /// Method adds product to user's wishlist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <returns>(Success, wishlistId)</returns>
        Task<(bool ok, long wishId)> AddProductToWishlistAsync(long userId, string productSKU);

        /// <summary>
        /// Method adds product to user's wishlist (this method is a bit faster than AddProductToWishlistAsync method)
        /// </summary>
        /// <param name="wishlistId"></param>
        /// <param name="productSKU"></param>
        /// <returns></returns>
        Task<bool> AddProductToWishlistByWishlistIdAsync(long wishlistId, string productSKU);

        Task<bool> CreateOrderFromCart(long cartId);

        Task<IEnumerable<VwProdProduct>> GetProducts();
    }
}
