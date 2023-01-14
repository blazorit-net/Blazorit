using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;

namespace Blazorit.Server.Services.Abstract.ECommerce.Domain {
    public interface IDataService {

        Task<IEnumerable<SubMenu>> GetMainHeaderMenu();
    }
}
