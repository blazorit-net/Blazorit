using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.ECommerce.Domain.Cart {
    public interface ICartService {
        Task<IEnumerable<VwShopcart>> AddProductToCartAsync(long userId, string productSKU, int quantity);
    }
}
