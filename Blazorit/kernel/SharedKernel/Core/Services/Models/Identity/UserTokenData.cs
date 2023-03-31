using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Core.Services.Models.Identity
{
    public class UserTokenData
    {
        public UserTokenData() { }

        public UserTokenData(long userId, string userName, string userRole)
        {
            UserId = userId;
            UserName = userName;
            UserRole = userRole;
        }

        public long UserId { get; set; }

        public string UserName { get; set; } = string.Empty;
        
        public string UserRole { get; set; } = string.Empty;
    }
}
