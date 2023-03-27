using Blazorit.Client.Services.Abstract.ECommerce.Domain.Payments;
using Blazorit.Client.Support.Helpers;
using Blazorit.Shared.Models.Universal;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _http;

        public PaymentService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Method returns payment methods
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
        {
            var result = await _http.GetFromJsonOrDefaultAsync<IEnumerable<PaymentMethod>>($"{PaymentApi.CONTROLLER}/{PaymentApi.GET_METHODS}");
            return result ?? Enumerable.Empty<PaymentMethod>();
        }
    }
}
