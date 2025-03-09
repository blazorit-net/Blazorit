using Blazorit.Domain.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using Blazorit.Server.Services.Abstract.ECommerce.Domain.Payments;

namespace Blazorit.Server.Services.Concrete.ECommerce.Domain.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly Core.UseCases.Services.Abstract.ECommerce.Domain.Payments.IPaymentService _paymentService;

        public PaymentService(Core.UseCases.Services.Abstract.ECommerce.Domain.Payments.IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Method returns payment methods
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
        {
            var result = await _paymentService.GetPaymentMethodsAsync();
            return result;
        }
    }
}
