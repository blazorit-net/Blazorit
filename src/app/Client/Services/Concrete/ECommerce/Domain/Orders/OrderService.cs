using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.Client.Support.Helpers;
using Blazorit.Shared.Models.Universal;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Orders
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _http;
        private readonly IIdentityService _ident;
        private readonly ICartService _cartService;

        public OrderService(HttpClient http, IIdentityService identService, ICartService cartService) 
        {
            _http = http;
            _ident = identService;
            _cartService = cartService;         
        }


        //public async Task CreateOrderFromCart()
        //{
        //    var result = await _http.PostAndReadAsJsonOrDefaultAsync<bool>($"{OrderApi.CONTROLLER}/{OrderApi.CREATE_ORDER}");
        //    await _cartService.SyncShopCartAsync(); // sync shopcart view state from kernel state
        //}


        /// <summary>
        /// Method creates order
        /// </summary>
        /// <param name="orderCreation"></param>
        /// <returns></returns>
        public async Task<Response<Order>> CreateOrder(PaidOrder orderCreation)
        {
            var result = await _http.PostAndReadAsJsonOrNewAsync<PaidOrder, Response<Order>>($"{OrderApi.CONTROLLER}/{OrderApi.CREATE_ORDER}", orderCreation);
            return result;
        }


        /// <summary>
        /// Method creates uniq token and info about order
        /// </summary>
        /// <param name="orderData"></param>
        /// <returns></returns>
        public async Task<Response<string>> CreateUniqOrderTokenAsync(CheckOrder orderData)
        {
            var result = await _http.PostAndReadAsJsonOrDefaultAsync<CheckOrder, Response<string>>($"{OrderApi.CONTROLLER}/{OrderApi.CREATE_ORDER_TOKEN}", orderData);
            return result ?? new Response<string>("Data transfer error");
        }
    }
}
