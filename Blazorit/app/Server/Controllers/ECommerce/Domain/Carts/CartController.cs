﻿using Blazorit.Server.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Shared.Models.ECommerce.Domain.Cart;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blazorit.Server.Controllers.ECommerce.Domain.Carts
{
    [Route(CartApi.CONTROLLER)]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpPost($"{CartApi.ADD_ITEM}")]
        public async Task<ActionResult<IEnumerable<VwShopcart>>> AddProductToCartAsync(CartItem cartItem) {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddProductToCartAsync(long.TryParse(userId, out long id) ? id : long.MinValue, cartItem.productSKU, cartItem.Quantity);

            if (result.Count() == 0) {
                return BadRequest(Enumerable.Empty<VwShopcart>());
            }

            return Ok(result);
        }


        [HttpGet($"{CartApi.GET_SHOPCART}")]
        public async Task<ActionResult<IEnumerable<VwShopcart>>> GetShopCartListAsync() {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.GetShopCartListAsync(long.TryParse(userId, out long id) ? id : long.MinValue);

            if (result.Count() == 0) {
                return BadRequest(Enumerable.Empty<VwShopcart>());
            }

            return Ok(result);
        }
    }
}