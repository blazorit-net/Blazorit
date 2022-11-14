using Blazorit.Server.Services.Abstract.Identity;
using Blazorit.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreServices = Blazorit.Core.Services;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.ident;


namespace Blazorit.Server.Services.Concrete.Identity {
    public class IdentityService : IIdentityService {
        private readonly CoreServices.Abstract.Identity.IIdentityService _identService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(CoreServices.Abstract.Identity.IIdentityService identService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) {
            _identService = identService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        //public string GetUserName() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);



        public async Task<ServiceResponse<string>> Login(string userName, string password) {            
            var responseFromCore = await _identService.CheckUser(userName, password);
            var response = new ServiceResponse<string>();

            response.Success = responseFromCore.Success;
            response.Message = responseFromCore.Message;

            if (response.Success == true) {
                response.Data = CreateToken(responseFromCore.Data.userId, responseFromCore.Data.userName, responseFromCore.Data.userRole);
            } 

            return response;
        }


        public async Task<ServiceResponse<long>> Register(string userName, string password) {
            var resultFromCore = await _identService.RegisterUser(userName, password);
            ServiceResponse<long> response;
            ConvertResponseFromCoreToServer(resultFromCore, out response);

            return response;
        }


        public async Task<ServiceResponse<bool>> ChangePassword(long userId, string newPassword) {
            var resultFromCore = await _identService.ChangeUserPassword(userId, newPassword);
            ServiceResponse<bool> response;
            ConvertResponseFromCoreToServer(resultFromCore, out response);

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
                .GetBytes(_configuration.GetSection("AppSettings:SecurityKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void ConvertResponseFromCoreToServer<T>(Blazorit.Core.Services.DTO.Identity.ServiceResult<T> from, out ServiceResponse<T> to) {
            to = new ServiceResponse<T>();
            to.Success = from.Success;
            to.Message = from.Message;
            to.Data = from.Data;
        }



        //public async Task<User> GetUserByUserName(string userName) {
        //    return await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        //}
    }
}
