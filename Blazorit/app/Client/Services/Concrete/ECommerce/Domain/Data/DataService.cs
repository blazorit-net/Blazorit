using Blazorit.Client.Services.Abstract.ECommerce.Domain.Data;
using Blazorit.Client.Services.Abstract.Identity;
using Blazorit.Shared.Models.ECommerce.Domain.Cart;
using Blazorit.Shared.Models.Universal;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;


namespace Blazorit.Client.Services.Concrete.ECommerce.Domain.Data
{
    public class DataService : IDataService
    {
        private readonly HttpClient _http;
        private readonly IIdentityService _ident;

        public DataService(HttpClient http, IIdentityService identService)
        {
            _http = http;
            _ident = identService;
        }

        public async Task<IEnumerable<SubMenu>> GetHeaderMenu()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<SubMenu>>("api/ecommerce/domain/data/header-menu");
            return result ?? new List<SubMenu>();
        }

        /// <summary>
        /// Method returns One product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        public async Task<ProductCardData> GetProductDataAsync(string category, string linkPart)
        {
            var result = await _http.GetFromJsonAsync<ProductCardData>($"{DataApi.CONTROLLER}/{DataApi.PRODUCT}/{category}/{linkPart}");
            return result ?? new ProductCardData();
        }


        ///// <summary>
        ///// Method returns picture's link parts for product card
        ///// </summary>
        ///// <param name="productId"></param>
        ///// <returns></returns>
        //public async Task<IEnumerable<PictureLinkPart>> GetPicturesLinkPartsForProductCardAsync(long productId) {
        //    var result = await _http.GetFromJsonAsync<IEnumerable<PictureLinkPart>>($"{DataApi.CONTROLLER}/{DataApi.PICTURES_LINK_PARTS}/{productId}");
        //    return result ?? new List<PictureLinkPart>();
        //}
    }
}
