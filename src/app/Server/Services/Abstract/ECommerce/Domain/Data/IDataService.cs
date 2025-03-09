using Blazorit.Domain.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.HeaderMenus;
using Blazorit.Domain.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;

namespace Blazorit.Server.Services.Abstract.ECommerce.Domain.Data
{
    public interface IDataService
    {

        Task<IEnumerable<SubMenu>> GetMainHeaderMenu();

        /// <summary>
        /// Method return data of one product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        Task<ProductCardData?> GetProductDataAsync(string category, string linkPart);


        ///// <summary>
        ///// Method returns picture's link parts for product card
        ///// </summary>
        ///// <param name="productId"></param>
        ///// <returns></returns>
        //Task<IEnumerable<PictureLinkPart>> GetPicturesLinkPartsForProductCardAsync(long productId);
    }
}
