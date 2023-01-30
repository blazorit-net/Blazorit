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
        /// <param name="quantity"></param>
        /// <returns>shopcart lis</returns>
        Task<IEnumerable<VwShopcart>> AddProductToCartAsync(string productSKU, int quantity);

        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<VwShopcart>> GetShopCartListAsync();
    }
}
