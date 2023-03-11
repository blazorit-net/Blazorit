using Blazorit.Client.Services.Abstract.ECommerce.Domain.Payments;
using Blazorit.Shared.Models.Universal;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Payments
{
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// Method create token with info about order (delivery, amount and other)
        /// </summary>
        /// <param name="paymentAmount"></param>
        /// <returns></returns>
        public async Task<Response<string>> CreateUniqPaymentToken(decimal paymentAmount)
        {
            return await Task.FromResult(new Response<string>());
        }


    }
}
