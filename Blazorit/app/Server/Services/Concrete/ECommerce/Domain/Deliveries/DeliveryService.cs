using Blazorit.Server.Services.Abstract.ECommerce.Domain.Deliveries;
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
        public async Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(long userId, long methodId)
        {
            var result = await _deliveryService.GetDeliveryAddresses(userId, methodId);
            return result;
        }
    }
}
