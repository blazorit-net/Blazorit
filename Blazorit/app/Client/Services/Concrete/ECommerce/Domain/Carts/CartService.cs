using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Shared.Models.ECommerce.Domain.Cart;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using System.Net.Http.Json;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Carts
{
    /// <summary>
    /// Server service for shop carts
    /// </summary>
    public class CartService : ICartService
    {
        private readonly HttpClient _http;
        private readonly IIdentityService _ident;


        public CartService(HttpClient http, IIdentityService identService)
        {
            _http = http;
            _ident = identService;
        }


        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="productSKU"></param>
        /// <param name="quantity"></param>
        /// <returns>shopcart lis</returns>
        public async Task<IEnumerable<VwShopcart>> AddProductToCartAsync(string productSKU, int quantity)
        {
            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth) {
                //TODO: check local storage for shopcart,
                //then to add shopcart from local storage to server-core storage,
                //then remove local storage

                CartItem cartItem = new() { productSKU = productSKU, Quantity = quantity };                
                var response = await _http.PostAsJsonAsync($"{CartApi.CONTROLLER}/{CartApi.ADD_ITEM}", cartItem);
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<VwShopcart>>();
                return result ?? Enumerable.Empty<VwShopcart>();
            } else {
                //TODO: add product to local storage
            }

            return Enumerable.Empty<VwShopcart>(); ;
        }
    }
}
