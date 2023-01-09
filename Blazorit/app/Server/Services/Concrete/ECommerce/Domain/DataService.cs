using Blazorit.Server.Services.Abstract.ECommerce.Domain;

namespace Blazorit.Server.Services.Concrete.ECommerce.Domain {
    public class DataService : IDataService {
        private readonly Core.Services.Abstract.ECommerce.Domain.IDataService _dataService;

        public DataService(Blazorit.Core.Services.Abstract.ECommerce.Domain.IDataService dataService) {
            _dataService = dataService;
        }

        public async Task<IEnumerable<string>> GetHeaderMenu() {
            return await _dataService.GetHeaderMenu();
        }
    }
}
