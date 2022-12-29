using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Infrastructure.Repositories.Abstract.ECommerce {
    public interface IECommerceRepository {
        Task<bool> AddProductToCart(long userId, string productSKU, int quantity);
    }
}
