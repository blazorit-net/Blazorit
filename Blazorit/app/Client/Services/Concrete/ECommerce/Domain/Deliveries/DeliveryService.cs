using Blazorit.Client.Services.Abstract.ECommerce.Domain.Deliveries;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Client.Support.Helpers;
using Blazorit.Shared.Models.ECommerce.Domain.Deliveries;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Deliveries;
using System.Net.Http.Json;

namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Deliveries
{
    public class DeliveryService : IDeliveryService
    {
        private readonly HttpClient _http;
        private readonly IIdentityService _ident;

        public DeliveryService(HttpClient http, IIdentityService identService)
        {
            _http = http;
            _ident = identService;
        }


        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods()
        {
            var result = await _http.GetFromJsonOrDefaultAsync<IEnumerable<DeliveryMethod>>($"{DeliveryApi.CONTROLLER}/{DeliveryApi.GET_METHODS}");
            return result ?? new List<DeliveryMethod>();
        }


        /// <summary>
        /// Method returns delivery addresses for user
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(long methodId)
        {
            var result = await _http.GetFromJsonOrDefaultAsync<IEnumerable<DeliveryAddress>>($"{DeliveryApi.CONTROLLER}/{DeliveryApi.GET_ADDRESSES}/{methodId}");
            return result ?? new List<DeliveryAddress>();
        }

        /// <summary>
        /// Method adds new delivery address for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="methodId"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> AddDeliveryAddressAsync(DeliveryMethod method, DeliveryAddress address)
        {
            var methodAddress = new MethodAddress { MethodId = method.Id, Address = address.Address };

            var result = await _http.PostAndReadAsJsonOrDefaultAsync<MethodAddress, IEnumerable<DeliveryAddress>>($"{DeliveryApi.CONTROLLER}/{DeliveryApi.ADD_ADDRESS}", methodAddress);
            return result ?? new List<DeliveryAddress>();
        }
    }
}
