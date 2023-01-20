using Blazorit.Server.Services.Abstract.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.ProductCards;
using Blazorit.Shared.Models.Universal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;

namespace Blazorit.Server.Controllers.ECommerce.Domain
{
    //[Route("api/ecommerce/domain/[controller]")]
    [Route(DataApi.CONTROLLER)]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }


        [HttpGet("header-menu")]
        public async Task<ActionResult<IEnumerable<SubMenu>>> HeaderMenu() {
            var menus = await _dataService.GetMainHeaderMenu();

            if (menus == null) {
                return BadRequest();
            }

            //foreach(var subMenu in menus) {
            //    foreach(var item in subMenu.MenuItems) {
            //        item.Link = $"/{DataApi.PRODUCT}/{item.Link}"; //add prefix link '/product/'
            //    }
            //}

            return Ok(menus);
        }


        /// <summary>
        /// Get One product
        /// </summary>
        /// <param name="category"></param>
        /// <param name="linkPart"></param>
        /// <returns></returns>
       // [HttpGet("product/{category}/{linkPart}")]
        [HttpGet($"{DataApi.PRODUCT}/{{category}}/{{linkPart}}")]
        public async Task<ActionResult<ProductCard>> Get(string category, string linkPart) {
            var prodCard = await _dataService.GetProductDataAsync(category, linkPart);
            if (prodCard == null) {
                return BadRequest(); // { Success = false, Message = "No product data available." });
            }

            return Ok(prodCard); // { Success = true, Data = prodCard });
        }
    }
}
