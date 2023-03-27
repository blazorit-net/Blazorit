﻿using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Payments;

namespace Blazorit.Client.Services.Abstract.ECommerce.Domain.Payments
{
    public interface IPaymentService
    {
        /// <summary>
        /// Method returns payment methods
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PaymentMethod>> GetPaymentMethodsAsync();
    }
}
