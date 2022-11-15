using Blazorit.Core.Services.Models.Identity;
using Blazorit.SharedKernel.Services.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Core.Services.Abstract.Identity {
    public interface IIdentityService {
        Task<ServiceResult<(long userId, string userName, string userRole)>> CheckUser(string userName, string password);
        Task<ServiceResult<long>> RegisterUser(string userName, string password);
        Task<ServiceResult<bool>> ChangeUserPassword(long userId, string newPassword);

        ////Task<(bool isOk, User)> GetUser(string userName);
        ////Task<(bool isOk, User)> GetUser(long userId);

        ////bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
