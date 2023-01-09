using Blazorit.Core.Services.Abstract.ECommerce.Domain;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain {
    public class DataService : IDataService {
        private readonly IECommerceRepository _dataRepo;

        public DataService(IECommerceRepository dataRepo) {
            _dataRepo = dataRepo;
        }

        //Test Method
        public async Task<IEnumerable<string>> GetHeaderMenu() {
            var items = await _dataRepo.GetProducts();

            return items?.Select(x => x.Name ?? string.Empty).ToList() ?? new List<string>();
        }
    }
}
