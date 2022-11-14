using Blazorit.Shared;

namespace Blazorit.Client.Services.Abstract.Identity
{
    public interface IIdentityService
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();
    }
}
