using Blazorit.Server.Services.Abstract.ECommerce.Admin.Products;
using Blazorit.Shared.Routes.WebAPI.ECommerce.Admin;
using Blazorit.SharedKernel.Core.IdentityRoles.Admin;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Admin.Products;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shr = Blazorit.Shared.Models.Universal;


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


        [HttpPost($"{ProductApi.ADD_PRODUCT}")]
        public async Task<ActionResult<Shr.Response<Product>>> AddProductAsync(Product product)
        {
            Product? result = await _productService.AddProductAsync(product);
            var response = new Shr.Response<Product>(result, "");
            return Ok(response);
        }


        [HttpPost($"{ProductApi.UPDATE_PRODUCT}")]
        public async Task<ActionResult<Product>> UpdateProductAsync(Product product)
        {
            var result = await _productService.UpdateProductAsync(product);

            if (result == null)
            {
                return Problem();
            }

            return Ok(result);
        }


        [HttpGet($"{ProductApi.GET_PRODUCTS}")]
        public async Task<ActionResult<List<Product>>> GetAllProductsAsync()
        {            
            var result = await _productService.GetAllProductsAsync();

            if (result.Count() == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpGet($"{ProductApi.GET_CATEGORIES}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesAsync()
        {
            IEnumerable<Category> result = await _productService.GetCategoriesAsync();

            if (result.Count() == 0)
            {
                return Problem();
            }

            return Ok(result);
        }

    }
}
