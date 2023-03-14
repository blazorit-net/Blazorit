using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Orders;
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

        ///// <summary>
        ///// Method adds product to user's cart by cartId (this method is a bit faster than AddProductToCartAsync method)
        ///// </summary>
        ///// <param name="cartId"></param>
        ///// <param name="productSKU"></param>
        ///// <param name="quantity"></param>
        ///// <returns>Success</returns>
        //Task<bool> AddProductToCartByCartIdAsync(long cartId, string productSKU, int quantity);

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
        /// Method creates payment info in repository
        /// </summary>
        /// <param name="paymentAmount"></param>
        /// <param name="manyParamsAboutPayments">you can extend your table for other fields, and this param must be deleted, and insert other params to method signature</param>
        /// <returns></returns>
        Task<(bool ok, long paymentId)> CreatePaymentInfoAsync(decimal paymentAmount, string? manyParamsAboutPayment = null);

        /// <summary>
        /// Method create or returns exists id of user delivery point
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="addressId"></param>
        /// <returns>user delivery point ID</returns>
        Task<(bool ok, long deliveryId)> InitUserDeliveryAsync(long userId, long methodId, long addressId);

        /// <summary>
        /// Method create order from cart for User by userId
        /// And this method removes all items from user's shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<bool> CreateOrderFromCart(long userId, long paymentId, long deliveryId);

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

        /// <summary>
        /// Method merges (sourceCart) shopcart with storage cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sourceCart"></param>
        /// <returns>Result cart</returns>
        Task<IEnumerable<VwShopcart>> UpdateShopCartAsync(long userId, IEnumerable<VwShopcart> sourceCart);

        /// <summary>
        /// Method returns all delivery methods
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();

        /// <summary>
        /// Method returns DeliveryAddresses for choosen delivery method for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <returns></returns>
        Task<IEnumerable<DeliveryAddress>> GetDeliveryAddressesAsync(long userId, long methodId);

        /// <summary>
        /// Method adds new delivery address for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<IEnumerable<DeliveryAddress>> AddDeliveryAddressAsync(long userId, long methodId, string address);

        /// <summary>
        /// Method returns common DeliveryAddresses for choosen delivery method
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        Task<IEnumerable<DeliveryAddress>> GetCommonDeliveryAddressesAsync(long methodId);


        /// <summary>
        /// Method returns User delivery (Id)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        Task<UserDelivery?> GetUserDelivery(long userId, long methodId, long addressId);


        /// <summary>
        /// Method creates uniq token and info about order
        /// </summary>
        /// <param name="paymentToken"></param>
        /// <param name="paymentAmount"></param>
        /// <param name="userId"></param>
        /// <param name="deliveryMethodId"></param>
        /// <param name="deliveryAddressId"></param>
        /// <returns></returns>
        Task<bool> CreateUniqOrderTokenAsync(string orderToken, decimal paymentAmount, long userId, long deliveryMethodId, long deliveryAddressId);

        /// <summary>
        /// Methods returns info about order by paymentToken
        /// </summary>
        /// <param name="paymentToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CheckoutOrder?> GetTokenOrderInfoAsync(string orderToken, long userId);
    }
}
