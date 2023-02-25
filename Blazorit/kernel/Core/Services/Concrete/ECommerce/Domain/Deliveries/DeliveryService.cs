using Blazorit.Core.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain.Deliveries
{
    public class DeliveryService : IDeliveryService
    {

        private readonly IECommerceRepository _dataRepo;

        public DeliveryService(IECommerceRepository dataRepo)
        {
            _dataRepo = dataRepo;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods()
        {
            IEnumerable<DeliveryMethod> result = await _dataRepo.GetDeliveryMethods();
            return result;
        }

        /// Method returns delivery addresses for user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(long userId, long methodId)
        {
            IEnumerable<DeliveryAddress> result = await _dataRepo.GetDeliveryAddresses(userId, methodId);
            return result;
        }
    }
}
