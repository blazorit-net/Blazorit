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
            bool isAuth = await _ident.IsUserAuthenticated();
            if (!isAuth) {
                return Enumerable.Empty<DeliveryMethod>();
            }

            var result = await _http.GetFromJsonOrDefaultAsync<IEnumerable<DeliveryMethod>>($"{DeliveryApi.CONTROLLER}/{DeliveryApi.GET_METHODS}");
            return result ?? new List<DeliveryMethod>();
        }


        /// <summary>
        /// Method returns delivery addresses for user
        /// </summary>
        /// <param name="methodId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses(DeliveryMethod method)
        {
            bool isAuth = await _ident.IsUserAuthenticated();
            if (!isAuth)
            {
                return Enumerable.Empty<DeliveryAddress>();
            }

            var result = await _http.GetFromJsonOrDefaultAsync<IEnumerable<DeliveryAddress>>($"{DeliveryApi.CONTROLLER}/{DeliveryApi.GET_ADDRESSES}/{method.Id}/{method.EnterAddress}");
            return result ?? new List<DeliveryAddress>();
        }

        /// <summary>
        /// Method adds new delivery address for user
        /// </summary>
        /// <param name="method"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DeliveryAddress>> AddDeliveryAddressAsync(DeliveryMethod method, string address)
        {
            bool isAuth = await _ident.IsUserAuthenticated();
            if (!isAuth)
            {
                return Enumerable.Empty<DeliveryAddress>();
            }

            MethodAddress methodAddress = new() { MethodId = method.Id, Address = address };

            var result = await _http.PostAndReadAsJsonOrDefaultAsync<MethodAddress, IEnumerable<DeliveryAddress>>($"{DeliveryApi.CONTROLLER}/{DeliveryApi.ADD_ADDRESS}", methodAddress);
            return result ?? new List<DeliveryAddress>();
        }
    }
}
