using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;

namespace Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts
{
    /// <summary>
    /// Client service for shop carts
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="productSKU"></param>
        /// <param name="product"></param>
        /// <returns>shopcart lis</returns>
        Task<ShopCart> AddProductToCartAsync(CartItem cartItem);

        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <returns></returns>
        Task<ShopCart> GetShopCartListAsync();

        
        /// <summary>
        /// Method merges shopcarts from local cart to server cart
        /// </summary>
        /// <returns></returns>
        Task<ShopCart> MergeLocalShopCartToServerShopCart();
    }
}
