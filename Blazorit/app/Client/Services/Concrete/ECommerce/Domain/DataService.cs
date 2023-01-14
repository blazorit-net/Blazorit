using Blazorit.Client.Services.Abstract.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using System.Net.Http.Json;


namespace Blazorit.Client.Services.Concrete.ECommerce.Domain {
    public class DataService : IDataService {
        private readonly HttpClient _http;

        public DataService(HttpClient http) {
            _http = http;
        }

        public async Task<IEnumerable<SubMenu>> GetHeaderMenu() {
            var result = await _http.GetFromJsonAsync<IEnumerable<SubMenu>>("api/ecommerce/domain/data/header-menu");
            return result ?? new List<SubMenu>(); 
        }
    }
}
