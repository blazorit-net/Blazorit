using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.ECommerce.Domain {
    public interface IDataService {
        /// <summary>
        /// Method returns the constructed menu
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SubMenu>> GetMainHeaderMenu();


        /// <summary>
        /// Method return data of one product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        Task<ProductCard?> GetProductDataAsync(string category, string linkPart);
    }
}
