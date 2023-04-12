using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;

namespace Blazorit.Server.Services.Abstract.ECommerce.Admin.Products
{
    public interface IProductService
    {
        /// <summary>
        /// Method adds product to products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product?> AddProductAsync(Product product);

        /// <summary>
        /// Method returns all products
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
