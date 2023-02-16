﻿using Blazorit.Client.Services.Abstract.ECommerce.Domain.Carts;
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
        private readonly CartState _cartState;

        public OrderService(HttpClient http, IIdentityService identService, ICartService cartService, CartState cartState) 
        {
            _http = http;
            _ident = identService;
            _cartService = cartService;
            _cartState = cartState;            
        }


        public async Task CreateOrderFromCart()
        {
            var result = await _http.PostAndReadAsJsonOrDefaultAsync<bool>($"{OrderApi.CONTROLLER}/{OrderApi.CREATE_ORDER}");
            _cartState.State = await _cartService.GetShopCartListAsync();
        }


        public async Task GetOrders()
        {

        }
    }
}