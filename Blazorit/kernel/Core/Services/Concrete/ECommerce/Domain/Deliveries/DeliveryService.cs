using Blazorit.Core.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries;
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
        public async Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(long userId, DeliveryMethod method)
        {
            IEnumerable<DeliveryAddress> result;
            
            if (method.EnterAddress)
            {
                result = await _dataRepo.GetDeliveryAddressesAsync(userId, method.Id);
            }
            else
            {
                result = await _dataRepo.GetCommonDeliveryAddressesAsync(method.Id);
            }
            
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
                return (await _dataRepo.GetDeliveryAddressesAsync(userId, methodId))
                    .OrderBy(x => x.DateTimeCreated);
            } 
            else if (address.Length > 200)
            {
                address = address.Substring(0, 200);
            }            

            IEnumerable<DeliveryAddress> result = await _dataRepo.AddDeliveryAddressAsync(userId, methodId, address);
                
            return result.OrderBy(x => x.DateTimeCreated);
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
            // TODO!!!
            // this stub code not for production. you need to implement logic code for address 
            IEnumerable<DeliveryMethod> methods = await _dataRepo.GetDeliveryMethodsAsync();
            if (methods.FirstOrDefault(x => x.Id == methodId)?.EnterAddress ?? false)
            {
                return new DeliveryCost(2); //stub 2$
            }            
            return new DeliveryCost(1); // stub 1$
        }
    }
}
