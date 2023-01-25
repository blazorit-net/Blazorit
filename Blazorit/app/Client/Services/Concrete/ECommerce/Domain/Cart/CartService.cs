using Blazorit.Client.Services.Abstract.ECommerce.Domain.Cart;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Shared.Models.ECommerce.Domain.Cart;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using System.Net.Http.Json;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Cart
{
    public class CartService : ICartService
    {
        private readonly HttpClient _http;
        private readonly IIdentityService _ident;

        public CartService(HttpClient http, IIdentityService identService)
        {
            _http = http;
            _ident = identService;
        }

        public async Task<IEnumerable<VwShopcart>> AddProductToCartAsync(string productSKU, int quantity)
        {
            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth)
            {
                CartItem cartItem = new() { productSKU = productSKU, Quantity = quantity };
                var result = await _http.PostAsJsonAsync($"{CartApi.CONTROLLER}/{CartApi.ADD_ITEM}", cartItem);
                var cartList = await result.Content.ReadFromJsonAsync<IEnumerable<VwShopcart>>();
                return cartList ?? Enumerable.Empty<VwShopcart>();
            }


            return Enumerable.Empty<VwShopcart>(); ;
        }
    }
}
