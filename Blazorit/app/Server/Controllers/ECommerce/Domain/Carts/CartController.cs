using Blazorit.Server.Services.Abstract.ECommerce.Domain.Carts;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
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


        [HttpGet($"{CartApi.GET_SHOPCART}")]
        public async Task<ActionResult<ShopCart>> GetShopCartListAsync() {
            long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;
            var result = await _cartService.GetShopCartListAsync(userId);

            if (result == null) {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPost($"{CartApi.ADD_ITEM}")]
        public async Task<ActionResult<ShopCart>> AddProductToCartAsync(CartItem cartItem) {
            long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;
            var result = await _cartService.AddProductToCartAsync(userId, cartItem);

            if (result == null) {
                return BadRequest();
            }

            return Ok(result);
        }


        [HttpPost($"{CartApi.MERGE_SHOPCARTS}")]
        public async Task<ActionResult<ShopCart>> MergeShopCarts(ShopCart clientCart) {
            long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;
            var result = await _cartService.MergeShopCarts(userId, clientCart);

            if (result == null) {
                return BadRequest();
            }

            return Ok(result);
        }


    }
}
