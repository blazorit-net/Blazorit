using Blazorit.Server.Services.Abstract.ECommerce.Domain.Carts;
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
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>shopcart list</returns>
        public async Task<IEnumerable<VwShopcart>> AddProductToCartAsync(long userId, string productSKU, int quantity)
        {
            var result = await _cartService.AddProductToCartAsync(userId, productSKU, quantity);
            return result;
        }
    }
}
