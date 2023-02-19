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
        /// In order not to once again update the state when logging in
        /// </summary>
        bool IsLoginingNow { get; set; }


        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        Task AddProductToCartAsync(CartItem cartItem);

        /// <summary>
        /// Method synchronizes shopcart's view state
        /// </summary>
        /// <returns></returns>
        Task SyncShopCartAsync();
        
        /// <summary>
        /// Method merges shopcarts from local cart to server cart
        /// </summary>
        /// <returns></returns>
        Task MergeLocalShopCartToServerShopCartAsync();

        /// <summary>
        /// Method clears local storage shopcart
        /// </summary>
        /// <returns></returns>
        ////Task ClearLocalShopcartAsync();

        /// <summary>
        /// Method sets local shopcart from server shopcart
        /// </summary>
        /// <returns></returns>
        Task SetLocalShopCartFromServerShopCart();
    }
}
