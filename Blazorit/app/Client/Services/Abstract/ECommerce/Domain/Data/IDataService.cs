using Blazorit.Shared.Models.Universal;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;

namespace Blazorit.Client.Services.Abstract.ECommerce.Domain.Data
{
    public interface IDataService
    {
        Task<IEnumerable<SubMenu>> GetHeaderMenu();
        Task<ProductCardData> GetProductDataAsync(string category, string linkPart);



        ///// <summary>
        ///// Method returns picture's link parts for product card
        ///// </summary>
        ///// <param name="productId"></param>
        ///// <returns></returns>
        //Task<IEnumerable<PictureLinkPart>> GetPicturesLinkPartsForProductCardAsync(long productId);
    }
}
