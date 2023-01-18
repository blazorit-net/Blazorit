using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;

namespace Blazorit.Client.Services.Abstract.ECommerce.Domain {
    public interface IDataService {
        Task<IEnumerable<SubMenu>> GetHeaderMenu();
        Task<string> GetProductData(string category, string linkPart);
    }
}
