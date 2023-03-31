using Blazorit.Core.Services.Models.Identity;
using Blazorit.SharedKernel.Core.Services.Models.Identity;

namespace Blazorit.Core.Services.Abstract.Identity {
    public interface IIdentityService {
        Task<IdentResult<UserTokenData>> CheckUser(string userName, string password);
        Task<IdentResult<long>> RegisterUser(string userName, string password);
        Task<IdentResult<bool>> ChangeUserPassword(long userId, string newPassword);

        ////Task<(bool isOk, User)> GetUser(string userName);
        ////Task<(bool isOk, User)> GetUser(long userId);

        ////bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
