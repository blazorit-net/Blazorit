using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.Client.Support.Helpers;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Carts
{
    /// <summary>
    /// Client service for shop carts
    /// </summary>
    public class CartService : ICartService
    {
        private readonly HttpClient _http;
        private readonly IIdentityService _ident;
        private readonly ILocalStorageService _localStorage;


        public CartService(HttpClient http, IIdentityService identService, ILocalStorageService localStorage)
        {
            _http = http;
            _ident = identService;
            _localStorage = localStorage;
        }


        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="productSKU"></param>
        /// <param name="product"></param>
        /// <returns>shopcart lis</returns>
        public async Task<ShopCart> AddProductToCartAsync(CartItem cartItem)
        {
            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth) 
            {
                HttpResponseMessage response = await _http.PostAsJsonAsync($"{CartApi.CONTROLLER}/{CartApi.ADD_ITEM}", cartItem);
                var result = await response.Content.ReadFromJsonAsync<ShopCart>();
                return result ?? new ShopCart();
            } 
            else //add product to local storage
            {
                ShopCart shopCart = await _localStorage.GetItemAsync<ShopCart>("shopcart") ?? new ShopCart();
                CartItem? item = shopCart.CartList.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
                if (item is null) 
                {
                    cartItem.DateTimeCreated = DateTimeOffset.Now;
                    shopCart.CartList.Add(cartItem);
                } else {
                    item.Quantity += cartItem.Quantity;
                }              
                
                await _localStorage.SetItemAsync<ShopCart>("shopcart", shopCart);
                return shopCart; //return from local storage
            }
        }


        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <returns></returns>
        public async Task<ShopCart> GetShopCartListAsync() 
        {
            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth) 
            {                
                var result = await _http.GetFromJsonOrDefaultAsync<ShopCart>($"{CartApi.CONTROLLER}/{CartApi.GET_SHOPCART}");
                return result ?? new ShopCart();
            } else 
            {
                var result = await _localStorage.GetItemAsync<ShopCart>("shopcart");
                return result ?? new ShopCart();
            }
        }



        public async Task<ShopCart> MergeLocalShopCartToServerShopCart() 
        {
            ShopCart localCart = await _localStorage.GetItemAsync<ShopCart>("shopcart") ?? new ShopCart();

            HttpResponseMessage response = await _http.PostAsJsonAsync($"{CartApi.CONTROLLER}/{CartApi.MERGE_SHOPCARTS}", localCart);
            var result = await response.Content.ReadFromJsonAsync<ShopCart>();
            return result ?? new ShopCart();
        }
    }
}
