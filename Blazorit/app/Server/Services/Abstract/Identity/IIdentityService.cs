using Blazorit.Shared.Models.Identity;

namespace Blazorit.Server.Services.Abstract.Identity
{
    public interface IIdentityService {
        Task<Response<string>> Login(string userName, string password);
        Task<Response<long>> Register(string userName, string password);
        Task<Response<bool>> ChangePassword(long userId, string newPassword);

        int GetUserId();
        string GetUserName();
    }
}
