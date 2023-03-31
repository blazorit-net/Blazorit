using Blazorit.Shared.Models.Identity;

namespace Blazorit.Server.Services.Abstract.Identity
{
    public interface IIdentityService {
        Task<IdentResponse<string>> Login(string userName, string password);
        Task<IdentResponse<long>> Register(string userName, string password);
        Task<IdentResponse<bool>> ChangePassword(long userId, string newPassword);

        int GetUserId();
        string GetUserName();
    }
}
