using Blazorit.Core.Services.Abstract.ECommerce.Domain.Payments;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IECommerceRepository _dataRepo;


        public PaymentService(IECommerceRepository dataRepo)
        {
            _dataRepo = dataRepo;
        }

        /// <summary>
        /// Method returns payment methods
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync()
        {
            var result = (await _dataRepo.GetPaymentMethodsAsync()).OrderBy(x => x.Ordby);
            return result;
        }


        /// <summary>
        /// Method returns payment method
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public async Task<PaymentMethod?> GetPaymentMethodAsync(long methodId)
        {
            var result = await _dataRepo.GetPaymentMethodAsync(methodId);
            return result;
        }
    }
}
