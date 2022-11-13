using Blazorit.Core.Services.Abstract.Identity;
using Blazorit.Core.Services.DTO.Identity;
using Blazorit.Infrastructure.Repositories.Abstract.Identity;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazorit.SharedKernel.Services.DTO.Identity;

namespace Blazorit.Core.Services.Concrete.Identity {
    public class IdentityService : IIdentityService {
        private readonly IIdentityRepository _identRepo;
        //private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IIdentityRepository identRepo) {  //IHttpContextAccessor httpContextAccessor) {
            _identRepo = identRepo;
            //_configuration = configuration;
            //_httpContextAccessor = httpContextAccessor;
        }


        public async Task<(bool isOk, User)> GetUser(string userName) {
            User? user = await _identRepo.GetUser(userName);
            if (user == null) {
                return (false, new User());
            }
            
            return (true, user);
        }


        public async Task<(bool isOk, User)> GetUser(long userId) {
            User? user = await _identRepo.GetUser(userId);
            if (user == null) {
                return (false, new User());
            }

            return (true, user);
        }


        public async Task<ServiceResult<(long userId, string userName, string userRole)>> CheckUser(string userName, string password) {
            var response = new ServiceResult<(long, string, string)>();
            var user = await _identRepo.GetUser(userName);
            if (user == null) {
                response.Success = false;
                response.Message = "User not found.";
            } else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
                response.Success = false;
                response.Message = "Wrong password.";
            } else {
                response.Success = true;
                response.Message = "User password matches";
                response.Data = (user.Id, user.UserName, user.Role);
            }

            return response;
        }


        public async Task<ServiceResult<long>> RegisterUser(string userName, string password) {
            //TODO: rewrite code: need for checking
            if (await _identRepo.UserExists(userName) == SharedKernel.Services.DTO.Identity.UserExistsResult.Exists) {
                return new ServiceResult<long> {
                    Success = false,
                    Message = "User already exists."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            //TODO: rewrite code: check result of RegisterUser
            var regRslt = await _identRepo.RegisterUser(userName, passwordHash, passwordSalt, "user_role");

            return new ServiceResult<long> { Data = regRslt.userId, Message = "Registration successful!" };
        }

        public async Task<ServiceResult<bool>> ChangeUserPassword(long userId, string newPassword) {
            var user = await _identRepo.GetUser(userId);
            if (user == null) {
                return new ServiceResult<bool> {
                    Success = false,
                    Message = "User not found."
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            await _identRepo.ChangeUserPassword(userId, passwordHash, passwordSalt);

            return new ServiceResult<bool> { Data = true, Message = "Password has been changed." };
        }


        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var hmac = new HMACSHA512(passwordSalt)) {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var hmac = new HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        //public async Task<User> GetUserByUserName(string userName) {
        //    return await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        //}
    }
}
