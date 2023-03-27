using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.ECommerce.Domain.Payments
{
    public interface IPaymentService
    {
        /// <summary>
        /// Method returns payment methods
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();

        /// <summary>
        /// Method returns payment method
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        Task<PaymentMethod?> GetPaymentMethodAsync(long methodId);
    }
}
