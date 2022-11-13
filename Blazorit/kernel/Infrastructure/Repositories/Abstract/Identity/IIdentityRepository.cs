using Blazorit.SharedKernel.Services.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Infrastructure.Repositories.Abstract.Identity {
    public interface IIdentityRepository {

        Task<UserExistsResult> UserExists(string userName);
        Task<(bool isOk, long userId)> RegisterUser(string userName, byte[] passwordHash, byte[] passwordSalt, string userRole);
        Task<User?> GetUser(string userName);
        Task<User?> GetUser(long userId);
        Task<bool> ChangeUserPassword(long userId, byte[] passwordHash, byte[] passwordSalt);


        //Task<ServiceResponse<int>> Register(User user, string password);
        //Task<UserExistsResult> UserExists(string userName);
        //Task<ServiceResponse<string>> Login(string userName, string password);
        //Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
        //int GetUserId();
        //string GetUserName();
        //Task<User> GetUserByUserName(string userName);
    }
}
