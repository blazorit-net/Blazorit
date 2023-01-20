using Blazorit.Core.Services.Abstract.ECommerce.Domain;
using Blazorit.Infrastructure.Repositories.Abstract.ECommerce;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Concrete.ECommerce.Domain {
    public class DataService : IDataService {
        private readonly IECommerceRepository _dataRepo;

        public DataService(IECommerceRepository dataRepo) {
            _dataRepo = dataRepo;
        }

        /// <summary>
        /// Method returns the constructed menu
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SubMenu>> GetMainHeaderMenu() {

            var products = await _dataRepo.GetProducts();

            if (products == null || !products.Any()) {
                return new List<SubMenu>();
            }

            List<SubMenu> mainMenu = new();

            foreach(var item in products) {                
                item.Category ??= "Others"; //Others category for others products                
            }

            //get all categories
            IEnumerable<string> categories = products
                .GroupBy(x => x.Category).Select(x => new { Category = x.Key ?? string.Empty })
                .Select(x => x.Category)
                .ToList();

            //designing the menu
            foreach (string category in categories) {
                var catProducts = products.Where(x => x.Category == category)
                    .Select(x => new MenuItem(x.Name, $"{category}/{x.LinkPart}".ToLower()))                    
                    .ToList();

                var subMenu = new SubMenu(category, catProducts);

                mainMenu.Add(subMenu);
            }

            return mainMenu;
        }


        /// <summary>
        /// Method return data of one product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
        public async Task<ProductCard?> GetProductDataAsync(string category, string linkPart) {
            var card = await _dataRepo.GetProductDataAsync(category, linkPart);

            if (card == null) {
                return null;
            }

            return new ProductCard {
                Category = card.Category,
                CategoryFullName = card.CategoryFullName,
                Id = card.Id,
                LinkPart = card.LinkPart ,
                Name = card.Name,
                Price = card.Price,
                Sku = card.Sku
            };
        }
    }
}
