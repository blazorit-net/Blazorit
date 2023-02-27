using Blazorit.Server.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Shared.Models.ECommerce.Domain.Deliveries;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blazorit.Server.Controllers.ECommerce.Domain.Deliveries
{
    [Route(DeliveryApi.CONTROLLER)]
    [Authorize]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet($"{DeliveryApi.GET_METHODS}")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
        {
            //long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;

            IEnumerable<DeliveryMethod> result = await _deliveryService.GetDeliveryMethods();

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpGet($"{DeliveryApi.GET_ADDRESSES}/{{methodId}}")]
        public async Task<ActionResult<IEnumerable<DeliveryAddress>>> GetDeliveryAddresses(long methodId)
        {
            long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;

            IEnumerable<DeliveryAddress> result = await _deliveryService.GetDeliveryAddresses(userId, methodId);

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPost($"{DeliveryApi.ADD_ADDRESS}")]
        public async Task<ActionResult<IEnumerable<DeliveryAddress>>> AddDeliveryAddressAsync(MethodAddress methodAddress)
        {
            long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;

            IEnumerable<DeliveryAddress> result = await _deliveryService.AddDeliveryAddressAsync(userId, methodAddress.MethodId, methodAddress.Address);

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
