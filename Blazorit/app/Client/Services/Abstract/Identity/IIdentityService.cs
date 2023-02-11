using Blazorit.Shared.Models.Identity;
using System.Threading.Tasks;

namespace Blazorit.Client.Services.Abstract.Identity
{
    public interface IIdentityService
    {
        Task<Response<int>> Register(UserRegister request);
        Task LoginAtClient(string? token);
        Task LogoutAsync();
        Task<Response<string>> LoginAtServer(UserLogin request);
        Task<Response<bool>> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();

    }
}
