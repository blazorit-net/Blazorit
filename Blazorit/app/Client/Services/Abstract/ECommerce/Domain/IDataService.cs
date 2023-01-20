using Blazorit.Shared.Models.Universal;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards;

namespace Blazorit.Client.Services.Abstract.ECommerce.Domain {
    public interface IDataService {
        Task<IEnumerable<SubMenu>> GetHeaderMenu();
        Task<ProductCard> GetProductDataAsync(string category, string linkPart);
    }
}
