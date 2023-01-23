﻿using Blazorit.Client.Services.Abstract.ECommerce.Domain;
using Blazorit.Shared.Models.Universal;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards;
using System.Net.Http.Json;


namespace Blazorit.Client.Services.Concrete.ECommerce.Domain
{
    public class DataService : IDataService {
        private readonly HttpClient _http;

        public DataService(HttpClient http) {
            _http = http;
        }

        public async Task<IEnumerable<SubMenu>> GetHeaderMenu() {
            var result = await _http.GetFromJsonAsync<IEnumerable<SubMenu>>("api/ecommerce/domain/data/header-menu");
            return result ?? new List<SubMenu>(); 
        }

        /// <summary>
        /// Method returns One product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        public async Task<ProductCardData> GetProductDataAsync(string category, string linkPart) {            
            var result = await _http.GetFromJsonAsync<ProductCardData>($"{DataApi.CONTROLLER}/{DataApi.PRODUCT}/{category}/{linkPart}");
            return result ?? new ProductCardData();
        }
    }
}
