using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards;

namespace Blazorit.Server.Services.Abstract.ECommerce.Domain {
    public interface IDataService {

        Task<IEnumerable<SubMenu>> GetMainHeaderMenu();

        /// <summary>
        /// Method return data of one product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        Task<ProductCardData?> GetProductDataAsync(string category, string linkPart);
    }
}
