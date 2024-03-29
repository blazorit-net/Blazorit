﻿using Blazorit.Shared.Models.Universal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Domain;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Domain.Products;
using Blazorit.Server.Services.Abstract.ECommerce.Domain.Data;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.HeaderMenus;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.Data.ProductCards;

namespace Blazorit.Server.Controllers.ECommerce.Domain.Data
{
    ////[Route("api/ecommerce/domain/[controller]")]
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
        public async Task<ActionResult<IEnumerable<SubMenu>>> HeaderMenu()
        {
            var menus = await _dataService.GetMainHeaderMenu();

            if (menus == null)
            {
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
        //// [HttpGet("product/{category}/{linkPart}")]
        [HttpGet($"{DataApi.PRODUCT}/{{category}}/{{linkPart}}")]
        public async Task<ActionResult<ProductCardData>> Get(string category, string linkPart)
        {
            var prodCard = await _dataService.GetProductDataAsync(category, linkPart);
            if (prodCard == null)
            {
                return BadRequest(); // { Success = false, Message = "No product data available." });
            }

            return Ok(prodCard); // { Success = true, Data = prodCard });
        }


        //[HttpGet($"{DataApi.PICTURES_LINK_PARTS}/{{productId}}")]
        //public async Task<ActionResult<IEnumerable<ProductCardData>>> GetPicturesLinkPartsForProductCardAsync(long productId) {
        //    var prodCard = await _dataService.GetPicturesLinkPartsForProductCardAsync(productId);
        //    return Ok(prodCard);
        //}
    }
}
