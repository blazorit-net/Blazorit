using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Blazorit.Infrastructure.Repositories.Abstract.Identity;
using Blazorit.SharedKernel.Services.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blazorit.Infrastructure.Repositories.Concrete.Identity {
    public class IdentityRepository : IIdentityRepository {

        private readonly IDbContextFactory<BlazoritContext> _contextFactory;


        public IdentityRepository(IDbContextFactory<BlazoritContext> contextFactory) {
            _contextFactory = contextFactory;
        }


        public async Task<UserExistsResult> UserExists(string userName) {            
            try {
                using (var context = _contextFactory.CreateDbContext()) {
                    if (await context.Users.AnyAsync(user => user.UserName.ToLower().Equals(userName.ToLower()))) {
                        return UserExistsResult.Exists;
                    }
                    return UserExistsResult.NotExists;
                }
            } catch {
                //TODO: log error
            }                     

            return UserExistsResult.Error;
        }


        public async Task<(bool isOk, long userId)> RegisterUser(string userName, byte[] passwordHash, byte[] passwordSalt, string userRole) {
            try {
                using (var context = _contextFactory.CreateDbContext()) {
                    var user = new DBStorages.BlazoritDB.EF.ident.User() {
                        ////Id = long.MinValue,
                        UserName = userName,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        DateCreated = DateTime.Now,
                        UserRole = userRole
                    };

                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return (true, user.Id);
                }
            } catch {
                //TODO: log error
            }            

            return (false, long.MinValue);
        }


        public async Task<User?> GetUser(string userName) {            
            try {
                using (var context = _contextFactory.CreateDbContext()) {
                    var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(userName.ToLower()));
                    if (user is null) return null;
                    return new User {
                        Id = user.Id,
                        UserName = user.UserName,
                        PasswordHash = user.PasswordHash,
                        PasswordSalt = user.PasswordSalt,
                        DateCreated = user.DateCreated,
                        Role = user.UserRole
                    };
                }
            } catch {
                //TODO: log error
            }            

            return null;
        }


        public async Task<User?> GetUser(long userId) {            
            try {
                using (var context = _contextFactory.CreateDbContext()) {
                    var user = await context.Users.FindAsync(userId);
                    if (user is null) return null;
                    return new User {
                        Id = user.Id,
                        UserName = user.UserName,
                        PasswordHash = user.PasswordHash,
                        PasswordSalt = user.PasswordSalt,
                        DateCreated = user.DateCreated,
                        Role = user.UserRole
                    };
                }
            } catch {
                //TODO: log error
            }            

            return null;
        }


        public async Task<bool> ChangeUserPassword(long userId, byte[] passwordHash, byte[] passwordSalt) {
            try {
                using (var context = _contextFactory.CreateDbContext()) {
                    var user = await context.Users.FindAsync(userId);
                    if (user is null) return false;

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    await context.SaveChangesAsync();
                    return true;
                }
            } catch {
                //TODO: log error
            }

            return false;
        }
    }
}
