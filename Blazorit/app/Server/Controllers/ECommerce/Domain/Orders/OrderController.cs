using Blazorit.Server.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blazorit.Server.Controllers.ECommerce.Domain.Orders
{
    [Route(OrderApi.CONTROLLER)]
    [Authorize]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost($"{OrderApi.CREATE_ORDER}")]
        public async Task<ActionResult<bool>> CreateOrderFromCart()
        {
            long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;
            bool result = await _orderService.CreateOrderFromCart(userId);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
