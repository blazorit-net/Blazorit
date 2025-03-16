using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazorit.Domain.SharedDomain.Core.Services.Models.ECommerce.Domain.Data.HeaderMenus;
using Blazorit.Domain.SharedDomain.Core.Services.Models.ECommerce.Domain.Data.ProductCards;

namespace Blazorit.Core.Services.Abstract.ECommerce.Domain.Data
{
    public interface IDataService
    {
        /// <summary>
        /// Method returns the constructed menu
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SubMenu>> GetMainHeaderMenu();


        /// <summary>
        /// Method returns data of one product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        Task<ProductCardData?> GetProductDataAsync(string category, string linkPart);


        /// <summary>
        ///// Method returns picture's link parts for product card
        ///// </summary>
        ///// <param name="productId"></param>
        ///// <returns></returns>
        //Task<IEnumerable<PictureLinkPart>> GetPicturesLinkPartsForProductCardAsync(long productId);
    }
}
