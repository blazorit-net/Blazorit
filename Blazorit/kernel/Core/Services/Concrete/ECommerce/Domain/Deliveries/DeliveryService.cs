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
            IEnumerable<DeliveryMethod> result = await _dataRepo.GetDeliveryMethodsAsync();
            return result;
        }

        /// Method returns delivery addresses for user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(long userId, long methodId)
        {
            IEnumerable<DeliveryAddress> result = await _dataRepo.GetDeliveryAddressesAsync(userId, methodId);
            return result;
        }

        /// <summary>
        /// Method adds new delivery address for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> AddDeliveryAddressAsync(long userId, long methodId, string address)
        {
            address = address.Trim();

            if (address == string.Empty)
            {
                return Enumerable.Empty<DeliveryAddress>();
            }

            IEnumerable<DeliveryAddress> result = await _dataRepo.AddDeliveryAddressAsync(userId, methodId, address);
            return result;
        }
    }
}
