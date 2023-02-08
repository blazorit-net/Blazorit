using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.Client.Support.Helpers;
using AntDesign.Core.Helpers.MemberPath;
using Blazorit.Client.States.ECommerce.Domain.Carts;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Carts
{
    /// <summary>
    /// Client service for shop carts
    /// </summary>
    public class CartService : ICartService
    {
        private const string LOCAL_SHOPCART = "shopcart";

        private readonly HttpClient _http;
        private readonly IIdentityService _ident;
        private readonly ILocalStorageService _localStorage;
        private readonly CartState _cartState;


        public CartService(HttpClient http, IIdentityService identService, ILocalStorageService localStorage, CartState cartState)
        {
            _http = http;
            _ident = identService;
            _localStorage = localStorage;
            _cartState = cartState;
        }


        /// <summary>
        /// In order not to once again update the state when logging in
        /// </summary>
        public bool IsLoginingNow { get; set; } = false;


        /// <summary>
        /// Method adds product (quantity of product) to shopcart
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public async Task<ShopCart> AddProductToCartAsync(CartItem cartItem)
        {
            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth) // add proudct to server shopcart
            {
                HttpResponseMessage response = await _http.PostAsJsonAsync($"{CartApi.CONTROLLER}/{CartApi.ADD_ITEM}", cartItem);
                var result = await response.Content.ReadFromJsonAsync<ShopCart>();
                return result ?? new ShopCart();
            } 
            else //add product to local storage
            {
                ShopCart localCart = await _localStorage.GetItemAsync<ShopCart>(LOCAL_SHOPCART) ?? new ShopCart();
                CartItem? item = localCart.CartList.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
                if (item is null) 
                {
                    cartItem.DateTimeCreated = DateTimeOffset.Now;
                    localCart.CartList.Add(cartItem);
                } 
                else 
                {
                    item.Quantity += cartItem.Quantity;
                }              
                
                await _localStorage.SetItemAsync(LOCAL_SHOPCART, localCart);
                return localCart; //return from local storage
            }
        }


        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <returns></returns>
        public async Task<ShopCart> GetShopCartListAsync() 
        {
            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth) // return from Server
            {                
                var result = await _http.GetFromJsonOrDefaultAsync<ShopCart>($"{CartApi.CONTROLLER}/{CartApi.GET_SHOPCART}");
                return result ?? new ShopCart();
            } 
            else // return from local storage
            {
                ShopCart localCart;
                try
                {
                    localCart = await _localStorage.GetItemAsync<ShopCart>(LOCAL_SHOPCART) ?? new ShopCart();
                }
                catch
                {
                    await Console.Error.WriteAsync($"Local storage shopcart error. Shopcart name '{nameof(LOCAL_SHOPCART)}'.");
                    await _localStorage.RemoveItemAsync(LOCAL_SHOPCART);
                    localCart = new ShopCart();
                }

                return localCart ?? new ShopCart();
            }
        }


        /// <summary>
        /// Method merges shopcarts from local cart to server cart
        /// </summary>
        /// <returns></returns>
        public async Task<ShopCart> MergeLocalShopCartToServerShopCartAsync() 
        {
            ShopCart localCart;
            try
            {
                localCart = await _localStorage.GetItemAsync<ShopCart>(LOCAL_SHOPCART) ?? new ShopCart();
            }
            catch
            {
                await Console.Error.WriteAsync($"Local storage shopcart error. Shopcart name '{nameof(LOCAL_SHOPCART)}'.");
                await _localStorage.RemoveItemAsync(LOCAL_SHOPCART);
                localCart = new ShopCart();
            }

            // merging on server
            HttpResponseMessage response = await _http.PostAsJsonAsync($"{CartApi.CONTROLLER}/{CartApi.MERGE_SHOPCARTS}", localCart);
            var result = await response.Content.ReadFromJsonAsync<ShopCart>();

            if (result != null) // remove local cart after merging on server 
            {
                await _localStorage.RemoveItemAsync(LOCAL_SHOPCART);
            }
            
            _cartState.State = result ?? new ShopCart();
            return _cartState.State;
        }


        /// <summary>
        /// Method clears local storage shopcart
        /// </summary>
        /// <returns></returns>
        public async Task ClearLocalShopcartAsync()
        {
            await _localStorage.RemoveItemAsync(LOCAL_SHOPCART);
        }


        /// <summary>
        /// Method sets local shopcart from server shopcart
        /// </summary>
        /// <returns></returns>
        public async Task<ShopCart> SetLocalShopcartFromServerShopCart()
        {
            var serverCart = await _http.GetFromJsonOrDefaultAsync<ShopCart>($"{CartApi.CONTROLLER}/{CartApi.GET_SHOPCART}");
            ShopCart localCart = serverCart ?? new ShopCart();
            await _localStorage.SetItemAsync(LOCAL_SHOPCART, localCart);
            _cartState.State = localCart;
            return _cartState.State;
        }
    }
}
