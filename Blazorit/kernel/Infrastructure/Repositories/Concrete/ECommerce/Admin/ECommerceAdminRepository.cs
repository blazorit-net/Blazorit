using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce.Admin;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Infrastructure.Repositories.Concrete.ECommerce.Admin
{
    public class ECommerceAdminRepository : IECommerceAdminRepository
    {
        private readonly IDbContextFactory<BlazoritContext> _contextFactory;
        private readonly ILogger? _logger;

        //public ECommerceRepository(IDbContextFactory<BlazoritContext> contextFactory) {
        //    _contextFactory = contextFactory;
        //}


        public ECommerceAdminRepository(IDbContextFactory<BlazoritContext> contextFactory, ILogger<ECommerceAdminRepository> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

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
        /// <returns>(Success, unique SKU)</returns>
        public async Task<(bool ok, string sku)> AddProductAsync(string productName, string curr, decimal price, string? description, string categoryName, string categoryFullName, string linkPart)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                description = string.IsNullOrEmpty(description?.Trim() ?? string.Empty) ? null : description; // null for description if it is empty

                string prefixSku = string.Empty;                

                ProdProduct product = new()
                {
                    Name = productName,
                    Curr = curr,
                    Price = price,
                    Description = description,
                    LinkPart = linkPart,
                };

                ProdCategory? category = await context.ProdCategories.FirstOrDefaultAsync(x => x.Name == categoryName);

                if (category is null)
                {
                    category = new ProdCategory
                    {
                        Name = categoryName,
                        FullName = categoryFullName,
                        PrefixSku = null
                    };
                    await context.ProdCategories.AddAsync(category);
                }

                if (!string.IsNullOrEmpty(category.PrefixSku))
                {
                    prefixSku = $"{category.PrefixSku}-";
                }

                product.Category = category;
                long maxProductId = context.ProdProducts.Max(x => x.Id);
                product.Sku = prefixSku + (1200 + (maxProductId + 1)).ToString(); //auto SKU (you can use any logic for auto SKU)

                await context.ProdProducts.AddAsync(product);
                await context.SaveChangesAsync();
                return (true, product.Sku);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(AddProductAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return (false, string.Empty);
        }


        /// <summary>
        /// Method returns product by SKU
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public async Task<VwProduct?> GetProductAsync(string sku)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();

                var product = await context.VwProdProducts
                    .Where(x => x.Sku == sku)
                    .Select(x => new VwProduct
                    {
                        Category = x.Category!,
                        CategoryFullName = x.CategoryFullName!,
                        Curr = x.Curr!,
                        DateCreate = x.DateCreate.GetValueOrDefault(),
                        DateModified = x.DateModified.GetValueOrDefault(),
                        DateTimeCreate = x.DateTimeCreate.GetValueOrDefault(),
                        DateTimeModified = x.DateTimeModified.GetValueOrDefault(),
                        Description = x.Description ?? string.Empty,
                        Id = x.Id.GetValueOrDefault(),
                        Name = x.Name!,
                        Price = x.Price.GetValueOrDefault(),
                        Sku = x.Sku!,
                        LinkPart = x.LinkPart!,
                        IsOnSite = x.IsOnSite ?? default
                    })
                    .FirstOrDefaultAsync();

                return product;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Error occurred in the method {nameof(GetProductAsync)} of the {nameof(ECommerceRepository)} repository");
            }

            return null;
        }
    }
}
