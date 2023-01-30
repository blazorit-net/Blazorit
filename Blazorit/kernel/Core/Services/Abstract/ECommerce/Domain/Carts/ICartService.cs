using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.ECommerce.Domain.Carts {
    /// <summary>
    /// Cart service for shopcarts
    /// </summary>
    public interface ICartService {
        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>shopcart list</returns>
        Task<IEnumerable<VwShopcart>> AddProductToCartAsync(long userId, string productSKU, int quantity);

        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<VwShopcart>> GetShopCartListAsync(long userId);
    }
}
