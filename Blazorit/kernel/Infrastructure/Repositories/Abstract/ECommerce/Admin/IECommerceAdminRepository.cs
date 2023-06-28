using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Infrastructure.Repositories.Abstract.ECommerce.Admin
{
    public interface IECommerceAdminRepository
    {
        /// <summary>
        /// Method adds product to products repository. This method auto assigns unique SKU for the product
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="curr"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="categoryName"></param>
        /// <param name="categoryFullName"></param>
        /// <param name="linkPart"></param>
        /// <param name="isOnSite"></param>
        /// <returns>(Success, unique SKU)</returns>
        Task<(bool ok, string sku)> AddProductAsync(string productName, string curr, decimal price, string? description, string categoryName, string categoryFullName, string linkPart, bool isOnSite);

        /// <summary>
        /// Method updates product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productName"></param>
        /// <param name="curr"></param>
        /// <param name="price"></param>
        /// <param name="description"></param>
        /// <param name="categoryName"></param>
        /// <param name="linkPart"></param>
        /// <param name="isOnSite"></param>
        /// <returns></returns>
        Task<bool> UpdateProductAsync(long id, string productName, string curr, decimal price, string? description, string categoryName, string linkPart, bool isOnSite);

        /// <summary>
        /// Method returns product by SKU
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        Task<VwProduct?> GetProductAsync(string sku);

        /// <summary>
        /// Method returns all products from repository
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<VwProduct>> GetAllProductsAsync();

        /// <summary>
        /// Method returns all categories
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
