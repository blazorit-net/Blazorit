using Blazorit.Core.Services.Abstract.ECommerce.Admin.Products;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce.Admin;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Admin.Products
{
    public class ProductService : IProductService
    {
        private readonly IECommerceAdminRepository _dataRepo;

        public ProductService(IECommerceAdminRepository dataRepo)
        {
            _dataRepo = dataRepo;
        }


        /// <summary>
        /// Method adds product to products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product?> AddProductAsync(Product product)
        {
            var repoResult = await _dataRepo.AddProductAsync(product.Name, product.Curr, 
                product.Price, product.Description, product.Category, product.CategoryFullName, product.LinkPart, product.IsOnSite);

            if (!repoResult.ok) 
            {
                return null;
            }

            VwProduct result = (await _dataRepo.GetProductAsync(repoResult.sku)) ?? new(); // check product in repository
            return new Product(result);
        }


        /// <summary>
        /// Method updates product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product?> UpdateProductAsync(Product product)
        {
            bool repoResult = await _dataRepo.UpdateProductAsync(product.Id, product.Name, product.Curr,
                product.Price, product.Description, product.Category, product.LinkPart, product.IsOnSite);

            if (!repoResult)
            {
                return null;
            }

            VwProduct result = (await _dataRepo.GetProductAsync(product.Sku)) ?? new(); // check product in repository
            return new Product(result);
        }

        /// <summary>
        /// Method returns all products
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var result = await _dataRepo.GetAllProductsAsync();
            return result.Select(x => new Product(x)).OrderByDescending(x => x.DateTimeModified).ToList();
        }


        /// <summary>
        /// Method returns all categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var result = await _dataRepo.GetCategoriesAsync();
            return result;
        }
    }
}
