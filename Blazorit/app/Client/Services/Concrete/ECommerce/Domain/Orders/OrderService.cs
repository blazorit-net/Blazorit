using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Client.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Client.States.ECommerce.Domain.Carts;
using Blazorit.Client.Support.Helpers;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;

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


        public async Task CreateOrderFromCart()
        {
            var result = await _http.PostAndReadAsJsonOrDefaultAsync<bool>($"{OrderApi.CONTROLLER}/{OrderApi.CREATE_ORDER}");
            await _cartService.SyncShopCartAsync(); // sync shopcart view state from kernel state
        }
    }
}
