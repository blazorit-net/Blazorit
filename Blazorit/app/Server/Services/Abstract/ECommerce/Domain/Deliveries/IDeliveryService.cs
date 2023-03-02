using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;

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
        Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(long userId, DeliveryMethod method);

        /// <summary>
        /// Method adds new delivery address for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<IEnumerable<DeliveryAddress>> AddDeliveryAddressAsync(long userId, long methodId, string address);
    }
}
