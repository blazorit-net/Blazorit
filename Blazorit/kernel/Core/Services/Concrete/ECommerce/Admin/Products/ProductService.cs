﻿using Blazorit.Core.Services.Abstract.ECommerce.Admin.Products;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce.Admin;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
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
                product.Price, product.Description, product.Category, product.CategoryFullName, product.LinkPart);

            if (!repoResult.ok) 
            {
                return null;
            }

            VwProduct result = (await _dataRepo.GetProductAsync(repoResult.sku)) ?? new(); // check product in repository
            return new Product(result);
        }
    }
}
