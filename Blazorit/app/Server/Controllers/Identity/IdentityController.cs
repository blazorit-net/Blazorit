using Blazorit.Server.Services.Abstract.Identity;
using Blazorit.Shared.Models.Identity;
using Blazorit.Shared.Routes.WebAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blazorit.Server.Controllers.Identity
{
    [Route(IdentApi.CONTROLLER)]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _authService;

        public IdentityController(IIdentityService authService)
        {
            _authService = authService;
        }


        [HttpPost($"{IdentApi.REGISTER}")]
        public async Task<ActionResult<IdentResponse<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(request.UserName, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost($"{IdentApi.LOGIN}")]
        public async Task<ActionResult<IdentResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpPost($"{IdentApi.CHANGE_PASSWORD}"), Authorize]
        public async Task<ActionResult<IdentResponse<bool>>> ChangePassword([FromBody] string newPassword)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var response = await _authService.ChangePassword(long.Parse(userId), newPassword);

            if (!response.Success || string.IsNullOrEmpty(userId))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
