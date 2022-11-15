using Blazorit.SharedKernel.Services.Models.Identity;

namespace Blazorit.Infrastructure.Repositories.Abstract.Identity {
    public interface IIdentityRepository {
        Task<UserExistsResult> UserExists(string userName);
        Task<(bool isOk, long userId)> RegisterUser(string userName, byte[] passwordHash, byte[] passwordSalt, string userRole);
        Task<User?> GetUser(string userName);
        Task<User?> GetUser(long userId);
        Task<bool> ChangeUserPassword(long userId, byte[] passwordHash, byte[] passwordSalt);
    }
}
