using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;

namespace Blazorit.Server.Services.Abstract.ECommerce.Domain.Carts
{
    /// <summary>
    /// Server service for shop carts
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        Task<ShopCart?> AddProductToCartAsync(long userId, CartItem cartItem);

        /// <summary>
        /// Method delte product from cart (cart by userId)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        Task<ShopCart?> DeleteProductFromCartAsync(long userId, CartItem cartItem);

        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ShopCart?> GetShopCartListAsync(long userId);


        /// <summary>
        /// Method merges client shopcart with kernel cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientCart"></param>
        /// <returns>Result cart</returns>
        Task<ShopCart?> MergeShopCarts(long userId, ShopCart clientCart);
    }
}
