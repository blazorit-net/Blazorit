using Blazorit.Server.Services.Abstract.ECommerce.Domain.Payments;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Blazorit.Server.Controllers.ECommerce.Domain.Payments
{
    [Route(PaymentApi.CONTROLLER)]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet($"{PaymentApi.GET_METHODS}")]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethodsAsync()
        {
            var result = await _paymentService.GetPaymentMethodsAsync();

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
