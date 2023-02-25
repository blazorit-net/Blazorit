﻿using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;

namespace Blazorit.Server.Services.Abstract.ECommerce.Domain.Deliveries
{
    public interface IDeliveryService
    {
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods();

        /// <summary>
        /// Method returns delivery addresses for user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <returns></returns>
        Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(long userId, long methodId);
    }
}
