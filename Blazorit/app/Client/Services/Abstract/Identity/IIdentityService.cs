using Blazorit.Shared.Models.Identity;

namespace Blazorit.Client.Services.Abstract.Identity
{
    public interface IIdentityService
    {
        Task<Response<int>> Register(UserRegister request);
        Task<Response<string>> Login(UserLogin request);
        Task<Response<bool>> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();
    }
}
