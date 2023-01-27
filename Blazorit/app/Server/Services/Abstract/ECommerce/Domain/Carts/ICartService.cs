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
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>shopcart list</returns>
        Task<IEnumerable<VwShopcart>> AddProductToCartAsync(long userId, string productSKU, int quantity);
    }
}
