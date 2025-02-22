using Blazorit.Server.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;

namespace Blazorit.Server.Services.Concrete.ECommerce.Domain.Deliveries
{
    public class DeliveryService : IDeliveryService
    {
        private readonly Core.Services.Abstract.ECommerce.Domain.Deliveries.IDeliveryService _deliveryService;

        public DeliveryService(Core.Services.Abstract.ECommerce.Domain.Deliveries.IDeliveryService deliveryService) 
        {
            _deliveryService = deliveryService;
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods()
        {
            var result = await _deliveryService.GetDeliveryMethods();
            return result;
        }

        /// Method returns delivery addresses for user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(long userId, DeliveryMethod method)
        {
            var result = await _deliveryService.GetDeliveryAddresses(userId, method);
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
            var result = await _deliveryService.AddDeliveryAddressAsync(userId, methodId, address);
            return result;
        }

        /// <summary>
        /// Method returns delivery cost
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<DeliveryCost> GetDeliveryCost(long userId, long methodId, string address)
        {
            var result = await _deliveryService.GetDeliveryCost(userId, methodId, address);
            return result;
        }
    }
}
