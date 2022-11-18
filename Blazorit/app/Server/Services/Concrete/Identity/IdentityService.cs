using Blazorit.Server.Services.Abstract.Identity;
using CoreServices = Blazorit.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazorit.Shared.Models.Identity;

namespace Blazorit.Server.Services.Concrete.Identity
{
    public class IdentityService : IIdentityService {
        private readonly CoreServices.Abstract.Identity.IIdentityService _identService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(CoreServices.Abstract.Identity.IIdentityService identService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) {
            _identService = identService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? int.MinValue.ToString());
        public string GetUserName() => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name) ?? string.Empty;


        public async Task<Response<string>> Login(string userName, string password) {            
            var responseFromCore = await _identService.CheckUser(userName, password);
            var response = new Response<string>();

            response.Success = responseFromCore.Success;
            response.Message = responseFromCore.Message;

            if (response.Success == true) {
                response.Data = CreateToken(responseFromCore.Data.userId, responseFromCore.Data.userName, responseFromCore.Data.userRole);
            } 

            return response;
        }


        public async Task<Response<long>> Register(string userName, string password) {
            var resultFromCore = await _identService.RegisterUser(userName, password);
            ConvertResponseFromCoreToServer(resultFromCore, out Response<long> response);

            return response;
        }


        public async Task<Response<bool>> ChangePassword(long userId, string newPassword) {
            var resultFromCore = await _identService.ChangeUserPassword(userId, newPassword);
            ConvertResponseFromCoreToServer(resultFromCore, out Response<bool> response);

            return response;
        }


        private string CreateToken(long userId, string userName, string userRole) {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, userRole)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:SecurityKey").Value ?? string.Empty));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        private void ConvertResponseFromCoreToServer<T>(CoreServices.Models.Identity.ServiceResult<T> from, out Response<T> to) {
            to = new Response<T>();
            to.Success = from.Success;
            to.Message = from.Message;
            to.Data = from.Data;
        }



        //public async Task<User> GetUserByUserName(string userName) {
        //    return await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        //}
    }
}
