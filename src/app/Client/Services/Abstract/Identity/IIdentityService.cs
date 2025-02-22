using Blazorit.Shared.Models.Identity;
using System.Threading.Tasks;

namespace Blazorit.Client.Services.Abstract.Identity
{
    public interface IIdentityService
    {
        Task<IdentResponse<int>> Register(UserRegister request);
        Task LoginAtClient(string? token);
        Task LogoutAsync();
        Task<IdentResponse<string>> LoginAtServer(UserLogin request);
        Task<IdentResponse<bool>> ChangePassword(UserChangePassword request);
        Task<bool> IsUserAuthenticated();

    }
}
