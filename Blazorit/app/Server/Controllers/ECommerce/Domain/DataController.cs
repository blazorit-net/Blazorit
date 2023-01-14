using Blazorit.Server.Services.Abstract.ECommerce.Domain;
using Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazorit.Server.Controllers.ECommerce.Domain
{
    [Route("api/ecommerce/domain/[controller]")]
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
            var response = await _dataService.GetMainHeaderMenu();

            if (response == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
