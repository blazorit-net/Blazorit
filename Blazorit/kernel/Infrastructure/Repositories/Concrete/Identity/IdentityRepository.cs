using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF;
using Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.ident;
using Blazorit.Infrastructure.Repositories.Abstract.Identity;
using Blazorit.SharedKernel.Services.DTO.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Infrastructure.Repositories.Concrete.Identity {
    public class IdentityRepository : IIdentityRepository {

        private readonly IDbContextFactory<BlazoritContext> _contextFactory;

        public IdentityRepository(IDbContextFactory<BlazoritContext> contextFactory) {
            this._contextFactory = contextFactory;
        }

        public async Task<UserExistsResult> UserExists(string userName) {
            using (var context = _contextFactory.CreateDbContext()) {
                try {
                    if (await context.Users.AnyAsync(user => user.UserName.ToLower().Equals(userName.ToLower()))) {
                        return UserExistsResult.Exists;
                    }
                    return UserExistsResult.NotExists;

                } catch {
                    //TODO: log error
                }
            }          

            return UserExistsResult.Error;
        }


        public async Task<(bool isOk, long userId)> RegisterUser(string userName, byte[] passwordHash, byte[] passwordSalt, string userRole) {
            using (var context = _contextFactory.CreateDbContext()) {
                try {
                    var user = new DBStorages.BlazoritDB.EF.ident.User() {
                        Id = long.MinValue,
                        UserName = userName,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        DateCreated = DateTime.Now,
                        Role = userRole
                    };

                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return (true, user.Id);

                } catch {
                    //TODO: log error
                }
            }

            return (false, long.MinValue);
        }


        public async Task<SharedKernel.Services.DTO.Identity.User?> GetUser(string userName) {
            using (var context = _contextFactory.CreateDbContext()) {
                try {
                    var user = await context.Users.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(userName.ToLower()));
                    return new SharedKernel.Services.DTO.Identity.User {
                        Id = user.Id,
                        UserName = user.UserName,
                        PasswordHash = user.PasswordHash,
                        PasswordSalt = user.PasswordSalt,
                        DateCreated = user.DateCreated,
                        Role = user.Role
                    };
                } catch {
                    //TODO: log error
                }
            }

            return null;
        }


        public async Task<bool> ChangeUserPassword(long userId, byte[] passwordHash, byte[] passwordSalt) {
            try {
                using (var context = _contextFactory.CreateDbContext()) {
                    var user = await context.Users.FindAsync(userId);
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
