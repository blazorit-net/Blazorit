using Blazorit.Client.Services.Abstract.ECommerce.Admin.Products;
using Blazorit.Client.Support.Helpers;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Admin;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products;
using Shr = Blazorit.Shared.Models.Universal;

namespace Blazorit.Client.Services.Concrete.ECommerce.Admin.Products
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
        public async Task<Shr.Response<Product>> AddProductAsync(Product product)
        {
            Shr.Response<Product> result = await _http.PostAndReadAsJsonOrNewAsync<Product, Shr.Response<Product>>($"{ProductApi.CONTROLLER}/{ProductApi.ADD_PRODUCT}", product);
            return result;
        }


        /// <summary>
        /// Method updates product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> UpdateProductAsync(Product product)
        {
            Product result = await _http.PostAndReadAsJsonOrNewAsync<Product, Product>($"{ProductApi.CONTROLLER}/{ProductApi.UPDATE_PRODUCT}", product);
            return result;
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            IEnumerable<Product> result = await _http.GetFromJsonOrNewAsync<List<Product>>($"{ProductApi.CONTROLLER}/{ProductApi.GET_PRODUCTS}");
            return result;
        }


        /// <summary>
        /// Method returns all categories
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            IEnumerable<Category> result = await _http.GetFromJsonOrNewAsync<List<Category>>($"{ProductApi.CONTROLLER}/{ProductApi.GET_CATEGORIES}");
            return result;
        }
    }
}
