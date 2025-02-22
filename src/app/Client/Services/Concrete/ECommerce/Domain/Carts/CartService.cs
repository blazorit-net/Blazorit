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
using Blazorit.Client.Pages.ECommerce.Domain.LayoutComponents.HeaderToolbars.Comps.Shopcarts;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Carts
{
    /// <summary>
    /// Client service for shop carts
    /// </summary>
    public class CartService : ICartService
    {
        private const string LOCAL_SHOPCART = "shopcart"; // local storage shopcart name

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
        public async Task AddProductToCartAsync(CartItem cartItem)
        {
            ShopCart resultCart = new();

            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth) // add proudct to server shopcart
            {
                var serverCart = await _http.PostAndReadAsJsonOrDefaultAsync<CartItem, ShopCart>($"{CartApi.CONTROLLER}/{CartApi.ADD_ITEM}", cartItem);

                if (serverCart == null) // if after adding item in cart -> cart is null
                {
                    await _ident.LogoutAsync(); // we need logout (server or core error, possible server unathorized user)
                }

                resultCart = serverCart ?? new ShopCart();
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
                    // check cart item for logic (zero or negative number) quantity 
                    if ((item.Quantity + cartItem.Quantity) > 0)
                    {
                        item.Quantity += cartItem.Quantity;
                    }
                }              
                
                await _localStorage.SetItemAsync(LOCAL_SHOPCART, localCart);                
                resultCart = localCart; //local storage
            }

            _cartState.State = resultCart;
            //return resultCart;
        }


        /// <summary>
        /// Method delte product from cart (cart by userId)
        /// </summary>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public async Task DeleteProductFromCartAsync(CartItem cartItem)
        {
            ShopCart resultCart = new();

            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth) // delete proudct from server
            {
                var serverCart = await _http.PostAndReadAsJsonOrNewAsync<CartItem, ShopCart>($"{CartApi.CONTROLLER}/{CartApi.DELETE_PRODUCT_ITEM}", cartItem);
                resultCart = serverCart;// ?? new ShopCart();
            }
            else // delete product from local storage
            {
                ShopCart localCart = await _localStorage.GetItemAsync<ShopCart>(LOCAL_SHOPCART) ?? new ShopCart();
                localCart.CartList.RemoveAll(x => x.ProductId == cartItem.ProductId);

                //CartItem? item = localCart.CartList.FirstOrDefault(x => x.ProductId == cartItem.ProductId);
                //if (item is not null)
                //{
                //    localCart.CartList.RemoveAll(x => x.ProductId == cartItem.ProductId);
                //}

                await _localStorage.SetItemAsync(LOCAL_SHOPCART, localCart);
                resultCart = localCart; //local storage
            }

            _cartState.State = resultCart;
        }


        /// <summary>
        /// Method receives shopcart
        /// </summary>
        /// <returns></returns>
        public async Task SyncShopCartAsync() 
        {
            ShopCart resultCart = new();

            bool isAuth = await _ident.IsUserAuthenticated();

            if (isAuth) // return from Server
            {                
                var result = await _http.GetFromJsonOrDefaultAsync<ShopCart>($"{CartApi.CONTROLLER}/{CartApi.GET_SHOPCART}");
                resultCart = result ?? new ShopCart();
                //return result ?? new ShopCart();
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
                    Console.WriteLine($"Blazorit: Local storage shopcart error. Shopcart name '{nameof(LOCAL_SHOPCART)}'. Shopcart will be removed.");
                    await _localStorage.RemoveItemAsync(LOCAL_SHOPCART);
                    localCart = new ShopCart();
                }

                resultCart = localCart ?? new ShopCart();
                //return localCart ?? new ShopCart();
            }

            _cartState.State = resultCart;
        }


        /// <summary>
        /// Method merges shopcarts from local cart to server cart
        /// </summary>
        /// <returns></returns>
        public async Task MergeLocalShopCartToServerShopCartAsync() 
        {
            ShopCart localCart;
            try
            {
                localCart = await _localStorage.GetItemAsync<ShopCart>(LOCAL_SHOPCART) ?? new ShopCart();
            }
            catch
            {
                Console.WriteLine($"Blazorit: Local storage shopcart error. Shopcart name '{nameof(LOCAL_SHOPCART)}'. Shopcart will be removed.");
                await _localStorage.RemoveItemAsync(LOCAL_SHOPCART);
                localCart = new ShopCart();
            }

            // merging on server
            var result = await _http.PostAndReadAsJsonOrDefaultAsync<ShopCart, ShopCart>($"{CartApi.CONTROLLER}/{CartApi.MERGE_SHOPCARTS}", localCart);
            await _localStorage.RemoveItemAsync(LOCAL_SHOPCART); //remove local shopcart            
            _cartState.State = result ?? new ShopCart();
            //return _cartState.State;
        }


        /// <summary>
        /// Method sets local shopcart from server shopcart
        /// </summary>
        /// <returns></returns>
        public async Task SetLocalShopCartFromServerShopCart()
        {
            var serverCart = await _http.GetFromJsonOrDefaultAsync<ShopCart>($"{CartApi.CONTROLLER}/{CartApi.GET_SHOPCART}");
            ShopCart localCart = serverCart ?? new ShopCart();
            await _localStorage.SetItemAsync(LOCAL_SHOPCART, localCart);
            _cartState.State = localCart;
            //return _cartState.State;
        }
    }
}
