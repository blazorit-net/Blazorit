using Blazorit.Core.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain.Carts {
    /// <summary>
    /// Cart service for shopcarts
    /// </summary>
    public class CartService : ICartService {
        private readonly IECommerceRepository _dataRepo;

        public CartService(IECommerceRepository dataRepo) {
            _dataRepo = dataRepo;
        }

        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>shopcart list</returns>
        public async Task<IEnumerable<VwShopcart>> AddProductToCartAsync(long userId, string productSKU, int quantity) {
            var resultRepo = await _dataRepo.AddProductToCartAsync(userId, productSKU, quantity);

            if (resultRepo.ok) {
                return await _dataRepo.GetShopCartListAsync(userId);
            }

            return Enumerable.Empty<VwShopcart>();
        }


        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VwShopcart>> GetShopCartListAsync(long userId) {
            return await _dataRepo.GetShopCartListAsync(userId);
        }
    }
}
