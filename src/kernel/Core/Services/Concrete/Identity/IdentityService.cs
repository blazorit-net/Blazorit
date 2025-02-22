using Blazorit.Core.Services.Abstract.Identity;
using Blazorit.Infrastructure.Repositories.Abstract.Identity;
using System.Security.Cryptography;
using Blazorit.SharedKernel.Infrastructure.Repositories.Models.Identity;
using Blazorit.SharedKernel.Core.IdentityRoles;

namespace Blazorit.Core.Services.Concrete.Identity
{
    public class IdentityService : IIdentityService {
        private readonly IIdentityRepository _identRepo;


        public IdentityService(IIdentityRepository identRepo) {
            _identRepo = identRepo;
        }


        public async Task<IdentResult<UserTokenData>> CheckUser(string userName, string password) {
            var response = new IdentResult<UserTokenData>();
            var user = await _identRepo.GetUser(userName);
            if (user == null) {
                response.Success = false;
                response.Message = "User not found";
            } else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
                response.Success = false;
                response.Message = "Wrong password";
            } else {
                response.Success = true;
                response.Message = "User password matches";
                response.Data = new UserTokenData(user.Id, user.UserName, user.Role);
            }

            return response;
        }


        public async Task<IdentResult<long>> RegisterUser(string userName, string password) {            
            var userExists = await _identRepo.UserExists(userName);
            if (userExists == UserExistsResult.Exists) {
                return new IdentResult<long> {
                    Success = false,
                    Message = "User already exists."
                };
            } else if (userExists == UserExistsResult.Error) {
                return new IdentResult<long> {
                    Success = false,
                    Message = "User check failed."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt); //local creating in memory
            
            var regRslt = await _identRepo.RegisterUser(userName, passwordHash, passwordSalt, "user_role");
            if (regRslt.isOk == true) {
                return new IdentResult<long> { 
                    Data = regRslt.userId, 
                    Success = true,
                    Message = "Registration successful!" };
            } else {
                return new IdentResult<long> {
                    Success = false,
                    Message = "User registration error."
                };
            }
        }


        public async Task<IdentResult<bool>> ChangeUserPassword(long userId, string newPassword) {
            var user = await _identRepo.GetUser(userId);
            if (user == null) {
                return new IdentResult<bool> {
                    Success = false,
                    Message = "User not found."
                };
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            if (await _identRepo.ChangeUserPassword(userId, passwordHash, passwordSalt)) {
                return new IdentResult<bool> { Success = true, Message = "Password has been changed." };
            }

            return new IdentResult<bool> { Success = false, Message = "Password hasn't been changed!" };
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) {
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


        ////public async Task<(bool isOk, User)> GetUser(string userName) {
        ////    User? user = await _identRepo.GetUser(userName);
        ////    if (user == null) {
        ////        return (false, new User());
        ////    }

        ////    return (true, user);
        ////}


        ////public async Task<(bool isOk, User)> GetUser(long userId) {
        ////    User? user = await _identRepo.GetUser(userId);
        ////    if (user == null) {
        ////        return (false, new User());
        ////    }

        ////    return (true, user);
        ////}


        ////public async Task<User> GetUserByUserName(string userName) {
        ////    return await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        ////}
    }
}
