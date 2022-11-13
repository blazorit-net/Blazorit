using Blazorit.Core.Services.DTO.Identity;
using Blazorit.SharedKernel.Services.DTO.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.Identity {
    public interface IIdentityService {
        ////Task<UserExistsResult> UserExists(string userName);

        Task<ServiceResult<(long userId, string userName, string userRole)>> CheckUser(string userName, string password);
        Task<ServiceResult<long>> RegisterUser(string userName, string password);
        Task<(bool isOk, User)> GetUser(string userName);
        Task<(bool isOk, User)> GetUser(long userId);
        Task<ServiceResult<bool>> ChangeUserPassword(long userId, string newPassword);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
