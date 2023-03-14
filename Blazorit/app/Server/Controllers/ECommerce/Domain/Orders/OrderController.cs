using Blazorit.Server.Services.Abstract.ECommerce.Domain.Orders;
using Blazorit.Shared.Models.Universal;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Carts;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders;
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



        [HttpPost($"{OrderApi.CREATE_ORDER_TOKEN}")]
        public async Task<ActionResult<Response<string>>> CreateUniqOrderTokenAsync(CheckOrder orderData)
        {
            long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;
            
            var result = await _orderService.CreateUniqOrderTokenAsync(userId, orderData);

            if (result.ok == false)
            {
                return BadRequest();
            }

            return Ok(new Response<string>(result.paymentToken, string.Empty));
        }


        [HttpPost($"{OrderApi.CREATE_ORDER}")]
        public async Task<ActionResult<bool>> CreateOrderAsync(OrderCreation orderCreation)
        {
            orderCreation.UserId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;

            var result = await _orderService.CreateOrder(orderCreation);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }

    }
}
