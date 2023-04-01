using Blazorit.Server.Services.Abstract.Identity;
using CoreServices = Blazorit.Core.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazorit.Shared.Models.Identity;
using Blazorit.SharedKernel.Core.IdentityRoles;

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


        /// <summary>
        /// Method check user and returns token (string)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IdentResponse<string>> Login(string userName, string password) {            
            var resultFromCore = await _identService.CheckUser(userName, password);
            var response = new IdentResponse<string>();

            response.Success = resultFromCore.Success;
            response.Message = resultFromCore.Message;

            if (resultFromCore.Success == true) {
                response.Data = CreateToken(resultFromCore.Data!);
            } 

            return response;
        }


        public async Task<IdentResponse<long>> Register(string userName, string password) {
            var resultFromCore = await _identService.RegisterUser(userName, password);
            ConvertResponseFromCoreToServer(resultFromCore, out IdentResponse<long> response);

            return response;
        }


        public async Task<IdentResponse<bool>> ChangePassword(long userId, string newPassword) {
            var resultFromCore = await _identService.ChangeUserPassword(userId, newPassword);
            ConvertResponseFromCoreToServer(resultFromCore, out IdentResponse<bool> response);

            return response;
        }


        private string CreateToken(UserTokenData userData) {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userData.UserId.ToString()),
                new Claim(ClaimTypes.Name, userData.UserName),
                new Claim(ClaimTypes.Role, userData.UserRole)
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


        private void ConvertResponseFromCoreToServer<T>(IdentResult<T> from, out IdentResponse<T> to) {
            to = new IdentResponse<T>();
            to.Success = from.Success;
            to.Message = from.Message;
            to.Data = from.Data;
        }



        //public async Task<User> GetUserByUserName(string userName) {
        //    return await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        //}
    }
}
