using Blazorit.Server.Services.Abstract.Identity;
using Blazorit.Shared.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blazorit.Server.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _authService;

        public IdentityController(IIdentityService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Response<int>>> Register(UserRegister request)
        {
            var response = await _authService.Register(request.UserName, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Response<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<Response<bool>>> ChangePassword([FromBody] string newPassword)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var response = await _authService.ChangePassword(int.Parse(userId), newPassword);

            if (!response.Success || string.IsNullOrEmpty(userId))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
