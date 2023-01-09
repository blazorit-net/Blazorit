using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.ECommerce.Domain {
    public interface IDataService {
        Task<IEnumerable<string>> GetHeaderMenu();
    }
}
