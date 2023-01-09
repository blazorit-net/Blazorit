using Blazorit.Server.Services.Abstract.ECommerce.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazorit.Server.Controllers.ECommerce {
    [Route("api/[controller]")]
    [ApiController]
    public class ECommerceController : ControllerBase {
        private readonly IDataService _dataService;

        public ECommerceController(IDataService dataService) {
            _dataService = dataService;
        }


        [HttpPost("header-menu")]
        public async Task<ActionResult<IEnumerable<string>>> Register() {
            var response = await _dataService.GetHeaderMenu();

            if (response == null) {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
