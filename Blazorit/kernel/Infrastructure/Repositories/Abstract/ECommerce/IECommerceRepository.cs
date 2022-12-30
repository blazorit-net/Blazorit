using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Infrastructure.Repositories.Abstract.ECommerce {
    public interface IECommerceRepository {
        Task<bool> AddProductToCart(long userId, string productSKU, int quantity);
        Task<(bool ok, string sku)> AddProduct(string name, string curr, decimal price, string? description, string? categoryName);
        Task<(bool ok, string sku)> AddProduct(string sku, string name, string curr, decimal price, string? description, string? categoryName);
    }
}
