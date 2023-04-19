using Blazorit.Server.Services.Abstract.ECommerce.Admin.Products;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products;

namespace Blazorit.Server.Services.Concrete.ECommerce.Admin.Products
{
    public class ProductService : IProductService
    {
        private readonly Core.Services.Abstract.ECommerce.Admin.Products.IProductService _productService;

        public ProductService(Core.Services.Abstract.ECommerce.Admin.Products.IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Method adds product to products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product?> AddProductAsync(Product product)
        {
            var result = await _productService.AddProductAsync(product);
            return result;
        }


        /// <summary>
        /// Method returns all products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var result = await _productService.GetAllProductsAsync();
            return result;
        }

        /// <summary>
        /// Method returns all categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var result = await _productService.GetCategoriesAsync();
            return result;
        }
    }
}
