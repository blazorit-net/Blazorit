using Blazorit.Core.Services.Abstract.ECommerce.Domain.Cart;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain.Cart {
    public class CartService : ICartService {
        private readonly IECommerceRepository _dataRepo;

        public CartService(IECommerceRepository dataRepo) {
            _dataRepo = dataRepo;
        }

        public async Task<IEnumerable<VwShopcart>> AddProductToCartAsync(long userId, string productSKU, int quantity) {
            var resultRepo = await _dataRepo.AddProductToCartAsync(userId, productSKU, quantity);

            if (resultRepo.ok) {
                return await _dataRepo.GetShopCartListAsync(userId);
            }

            return Enumerable.Empty<VwShopcart>();
        }
    }
}
