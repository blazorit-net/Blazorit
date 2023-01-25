using Blazorit.Server.Services.Abstract.ECommerce.Domain.Cart;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;

namespace Blazorit.Server.Services.Concrete.ECommerce.Domain.Cart
{
    public class CartService : ICartService
    {
        private readonly Core.Services.Abstract.ECommerce.Domain.Cart.ICartService _cartService;

        public CartService(Core.Services.Abstract.ECommerce.Domain.Cart.ICartService cartService)
        {
            _cartService = cartService;
        }


        public async Task<IEnumerable<VwShopcart>> AddProductToCartAsync(long userId, string productSKU, int quantity)
        {
            var result = await _cartService.AddProductToCartAsync(userId, productSKU, quantity);
            return result;
        }
    }
}
