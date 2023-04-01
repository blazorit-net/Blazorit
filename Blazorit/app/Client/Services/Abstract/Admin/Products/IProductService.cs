using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;

namespace Blazorit.Client.Services.Abstract.Admin.Products
{
    public interface IProductService
    {
        /// <summary>
        /// Method adds product to products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product> AddProductAsync(Product product);
    }
}
