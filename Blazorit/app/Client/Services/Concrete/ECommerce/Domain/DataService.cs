using Blazorit.Client.Services.Abstract.ECommerce.Domain;
using System.Net.Http.Json;


namespace Blazorit.Client.Services.Concrete.ECommerce.Domain {
    public class DataService : IDataService {
        private readonly HttpClient _http;

        public DataService(HttpClient http) {
            _http = http;
        }

        public async Task<IEnumerable<string>> GetHeaderMenu() {
            var result = await _http.GetFromJsonAsync<IEnumerable<string>>("api/ecommerce/header-menu");
            return result ?? new List<string>(); //result.Content.ReadFromJsonAsync<Response<bool>>() ?? new Response<bool> { Success = false, Message = "Error response" };
        }
    }
}
