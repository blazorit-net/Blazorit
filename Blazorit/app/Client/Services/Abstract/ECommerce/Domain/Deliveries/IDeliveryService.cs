using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;

namespace Blazorit.Client.Services.Abstract.ECommerce.Domain.Deliveries
{
    public interface IDeliveryService
    {
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods();

        /// <summary>
        /// Method returns delivery addresses for user
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(DeliveryMethod method);

        /// <summary>
        /// Method adds new delivery address for user
        /// </summary>
        /// <param name="method"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<IEnumerable<DeliveryAddress>> AddDeliveryAddressAsync(DeliveryMethod method, string address);

        /// <summary>
        /// Method returns delivery cost
        /// </summary>
        /// <param name="method"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<DeliveryCost> GetDeliveryCost(DeliveryMethod method, DeliveryAddress deliveryAddress);
    }
}
