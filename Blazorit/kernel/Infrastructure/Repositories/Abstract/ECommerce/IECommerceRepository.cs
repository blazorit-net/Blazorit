using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Infrastructure.Repositories.Abstract.ECommerce
{
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
        /// Method adds product to user's cart by product SKU
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>(Success, cartId)</returns>
        Task<(bool ok, long cartId)> AddProductToCartAsync(long userId, string productSKU, int quantity);

        /// <summary>
        /// Method adds product to user's cart by productId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<(bool ok, long cartId)> AddProductToCartAsync(long userId, long productId, int quantity);

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

        /// <summary>
        /// Method create order from cart for User by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> CreateOrderFromCart(long userId);

        /// <summary>
        /// Method returns all products from product's view
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<VwProduct>> GetProducts();


        /// <summary>
        /// Method return data of one product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        Task<VwProduct?> GetProductDataAsync(string category, string linkPart);


        /// <summary>
        /// Method returns picture's link parts of one product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="pic_size"></param>
        /// <param name="site_location"></param>
        /// <returns></returns>
        Task<IEnumerable<PictureLinkPart>> GetProductPictureLinkPartsAsync(long productId, string pic_size, string site_location);


        /// <summary>
        /// Method returns all user's items from shop cart 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<VwShopcart>> GetShopCartListAsync(long userId);


        Task<IEnumerable<VwShopcart>> UpdateShopCart(long userId, IEnumerable<VwShopcart> sourceCart);
    }
}
