using Blazorit.Server.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;

namespace Blazorit.Server.Services.Concrete.ECommerce.Domain.Carts
{
    /// <summary>
    /// Server service for shop carts
    /// </summary>
    public class CartService : ICartService
    {
        private readonly Core.Services.Abstract.ECommerce.Domain.Carts.ICartService _cartService;

        public CartService(Core.Services.Abstract.ECommerce.Domain.Carts.ICartService cartService)
        {
            _cartService = cartService;
        }


        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ShopCart?> GetShopCartListAsync(long userId) 
        {
            var result = await _cartService.GetShopCartListAsync(userId);
            return result;
        }


        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>shopcart list</returns>
        public async Task<ShopCart?> AddProductToCartAsync(long userId, CartItem cartItem)
        {
            var result = await _cartService.AddProductToCartAsync(userId, cartItem.Sku, cartItem.Quantity);
            return result;
        }


        /// <summary>
        /// Method merges client shopcart with kernel cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientCart"></param>
        /// <returns>Result cart</returns>
        public async Task<ShopCart?> MergeShopCarts(long userId, ShopCart clientCart) 
        {
            var result = await _cartService.MergeShopCarts(userId, clientCart);
            return result;
        }
    }
}
