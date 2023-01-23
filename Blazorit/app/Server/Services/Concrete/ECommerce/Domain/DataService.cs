
using Blazorit.Server.Services.Abstract.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards;

namespace Blazorit.Server.Services.Concrete.ECommerce.Domain {
    public class DataService : IDataService {
        private readonly Core.Services.Abstract.ECommerce.Domain.IDataService _dataService;

        public DataService(Blazorit.Core.Services.Abstract.ECommerce.Domain.IDataService dataService) {
            _dataService = dataService;
        }

        /// <summary>
        /// Method returns the constructed menu
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SubMenu>> GetMainHeaderMenu() {
            var result = await _dataService.GetMainHeaderMenu();
            return result;
        }


        /// <summary>
        /// Method return data of one product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        public async Task<ProductCardData?> GetProductDataAsync(string category, string linkPart) {
            var result = await _dataService.GetProductDataAsync(category, linkPart);
            return result;
        }
    }

        //    /// <summary>
        ///// Method returns the constructed menu
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IEnumerable<SubMenu>> GetMainHeaderMenu() {
        //    var coreMainMenu =  await _dataService.GetMainHeaderMenu();

        //    //List<SubMenu> resultMenu = new();

        //    var result = coreMainMenu
        //        .Select(x => new SubMenu(
        //                x.Title, 
        //                new List<MenuItem>(x.MenuItems.Select(y => new MenuItem(y.Title, y.Link)))
        //            )
        //        )
        //        .ToList();
            
        //    //var result = ((IEnumerable<SubMenu>)res).ToList();
            
            
        //    return result;
        //}
  
}
