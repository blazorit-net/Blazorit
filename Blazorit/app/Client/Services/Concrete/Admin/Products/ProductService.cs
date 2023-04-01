﻿using Blazorit.Client.Services.Abstract.Admin.Products;
using Blazorit.Client.Support.Helpers;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Admin;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;

namespace Blazorit.Client.Services.Concrete.Admin.Products
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;


        public ProductService(HttpClient http)
        {
            _http = http;
        }


        /// <summary>
        /// Method adds product to products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> AddProductAsync(Product product)
        {
            Product result = await _http.PostAndReadAsJsonOrNewAsync<Product, Product>($"{ProductApi.CONTROLLER}/{ProductApi.ADD_PRODUCT}", product);
            return result;
        }
    }
}
