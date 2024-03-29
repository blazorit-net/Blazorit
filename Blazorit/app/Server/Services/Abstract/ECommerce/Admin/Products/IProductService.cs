﻿using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products;

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
        /// Method updates product to products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product?> UpdateProductAsync(Product product);

        /// <summary>
        /// Method returns all products
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();

        /// <summary>
        /// Method returns all categories
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
