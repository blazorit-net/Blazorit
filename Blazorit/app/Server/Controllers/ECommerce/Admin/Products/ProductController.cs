using Blazorit.Server.Services.Abstract.ECommerce.Admin.Products;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Admin;
using Blazorit.SharedKernel.Core.IdentityRoles.Admin;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blazorit.Server.Controllers.ECommerce.Admin.Products
{
    [Route(ProductApi.CONTROLLER)]
    [Authorize(Roles = AdminRole.ADMIN)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        //[HttpGet($"{CartApi.GET_SHOPCART}")]
        //public async Task<ActionResult<ShopCart>> GetShopCartListAsync()
        //{
        //    long userId = long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out long id) ? id : long.MinValue;
        //    var result = await _cartService.GetShopCartListAsync(userId);

        //    if (result == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}


        [HttpPost($"{ProductApi.ADD_PRODUCT}")]
        public async Task<ActionResult<Product>> AddProductAsync(Product product)
        {
            var result = await _productService.AddProductAsync(product);

            if (result == null)
            {
                return Problem();
            }

            return Ok(result);
        }

    }
}
