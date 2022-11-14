using Blazorit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.Server.Services.Abstract.Identity {
    public interface IIdentityService {
        Task<ServiceResponse<string>> Login(string userName, string password);
        Task<ServiceResponse<long>> Register(string userName, string password);
        Task<ServiceResponse<bool>> ChangePassword(long userId, string newPassword);

        //int GetUserId();
        //string GetUserName();
    }
}
